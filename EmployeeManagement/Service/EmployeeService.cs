using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Service
{
    public class EmployeeService : IEmployeeService

    {
        private readonly EmployeeContext _context;
        private readonly IMapper _mapper;
        private readonly string secretKey;
        
        public EmployeeService(EmployeeContext context,IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("Jwt:Key");
        }
        public async Task<String> AddNewEmployee(CreateEmployeeDTO employeeDto)
        {
            if (employeeDto == null) throw new ArgumentNullException("Please check the input vallues");
            
            string lastNamePart = employeeDto.Name.Substring(employeeDto.Name.Length - 4);
            string phoneLastPart = employeeDto.Phone.Substring(employeeDto.Phone.Length - 4);
            var employee = _mapper.Map<Employee>(employeeDto);
            
            //employee.Password = lastNamePart + phoneLastPart;
            var password = BCrypt.Net.BCrypt.HashPassword(lastNamePart + phoneLastPart, 10);
            employee.Password = password;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            string message = "You have successfully registerd your password should be last 4 letter of name and last 4 digit of number ";
            return message;
        }


        public async Task<bool> DeleteEmployeeById(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e=>e.Id==id);
            if(employee == null)
            {
                return false;
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            if(employees == null)
            {
                return null;
            }
            return employees;
        }
        
        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Include(p=>p.Job).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDept(int deptId)
        {
            var employees = await _context.Employees.Where(e => e.Job.DepartmentId == deptId).ToListAsync();
            if (employees == null)
            {
                return null;
            }
            return employees;
        }

        

        public  bool IsUniquePhonenumber(string phonenumber)
        {
            var employee =  _context.Employees.FirstOrDefault(e => e.Phone == phonenumber);
            if (employee == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginReqDTO loginReqDTO)
        {
            var user = await _context.Employees.Include(p=>p.Job).ThenInclude(p=>p.Department).FirstOrDefaultAsync(u => u.Phone == loginReqDTO.PhoneNo);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginReqDTO.Password, user.Password))
            {
                throw new Exception("Invalid Credential");
            }
            var tokenHandler = new JwtSecurityTokenHandler();   
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Job.Department.Name),
                    new Claim("Title",user.Job.JobTitle)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var userDto = _mapper.Map<CreateEmployeeDTO>(user);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO responseDTO = new LoginResponseDTO
            {
                token = tokenHandler.WriteToken(token),
                EmplyeeData = userDto
            };
            return responseDTO;
        }

        public async Task<Employee> UpdateEmployeeById(int id,CreateEmployeeDTO employee)
        {
            var _employee = await _context.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (_employee == null)
            {
                throw new Exception($"Invalid id {id}");
            }

            _mapper.Map(employee, _employee);

            await _context.SaveChangesAsync();
            return _employee;

        }
    }
}
