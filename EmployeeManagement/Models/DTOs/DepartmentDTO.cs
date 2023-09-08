using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DTOs
{
    public class DepartmentDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
