using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DTOs
{
    public class JobDTO
    {
        [Required]
        public string JobTitle { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
