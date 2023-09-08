using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee>AddNewEmployee(CreateEmployeeDTO employee);
        Task<Employee> UpdateEmployeeById(int id,CreateEmployeeDTO employee);
        Task<bool> DeleteEmployeeById(int id);
        Task<LoginResponseDTO> Login(LoginReqDTO loginReqDTO);
        Task<IEnumerable<Employee>> GetEmployeesByDept(int deptId);

    }
}
