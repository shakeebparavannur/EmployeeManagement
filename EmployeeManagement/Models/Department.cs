namespace EmployeeManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
