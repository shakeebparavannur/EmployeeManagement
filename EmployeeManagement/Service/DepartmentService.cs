using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeContext context;
        private readonly IMapper mapper;

        public DepartmentService(EmployeeContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }
        public async Task<Department> AddNewDepartment(DepartmentDTO department)
        {
            if (department == null) throw new ArgumentNullException("unable to add data");
            var depDto = mapper.Map<Department> (department);
            await context.Departments.AddAsync (depDto);
            await context.SaveChangesAsync();
            return depDto;
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            var departmentList = await context.Departments.ToListAsync();
            if(departmentList == null)
            {
                throw new Exception("data not found");
            }
            return departmentList;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var dept = await context.Departments.Include(d=>d.Jobs).SingleOrDefaultAsync(d=>d.DepartmentId== id);
            if (dept == null)
            {
                throw new Exception("data not found");

            }
            return dept;
        }

        public async Task<bool> RemoveDepartment(int id)
        {
            var dept = await context.Departments.FirstOrDefaultAsync(d=>d.DepartmentId==id);
            if(dept == null)
            {
                return false;
            }
            context.Departments.Remove(dept);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Department> UpdateDepartment(int id, DepartmentDTO department)
        {
            var dept = await context.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);
            if(dept == null)
            {
                throw new Exception("Data not found");
            }
            mapper.Map(department, dept);
            await context.SaveChangesAsync();
            return dept;
        }
    }
}
