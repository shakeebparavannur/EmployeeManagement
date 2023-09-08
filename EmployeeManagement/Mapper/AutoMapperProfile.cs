using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee,CreateEmployeeDTO>().ReverseMap();
            CreateMap<Job,JobDTO>().ReverseMap();
            CreateMap<Department,DepartmentDTO>().ReverseMap();
        }
    }
}
