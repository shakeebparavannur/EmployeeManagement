namespace EmployeeManagement.Models.DTOs
{
    public class LoginResponseDTO
    {
        public CreateEmployeeDTO EmplyeeData { get; set; }
        public string token { get; set; }
    }
}
