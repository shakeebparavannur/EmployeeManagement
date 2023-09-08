using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public int JobId { get; set; }
        
        public DateTime? JoinedDate { get; set; }
    }
}
