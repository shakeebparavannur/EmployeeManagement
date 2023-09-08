using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.DTOs
{
    public class LoginReqDTO
    {
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
