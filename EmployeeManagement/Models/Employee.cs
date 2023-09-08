using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        
        public int JobId { get; set; }
        public DateTime? JoinedDate { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual Job Job { get; set; }
        


    }
}
