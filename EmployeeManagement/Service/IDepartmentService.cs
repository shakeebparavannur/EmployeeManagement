using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Service
{
    public interface IDepartmentService
    {
        Task<Department> AddNewDepartment(DepartmentDTO department);
        Task<bool> RemoveDepartment(int id);
        Task<Department> UpdateDepartment(int id, DepartmentDTO department);
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<Department> GetDepartmentById(int id);
    }
}
