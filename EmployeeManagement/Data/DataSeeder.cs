using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public class DataSeeder
    {
        /// <summary>
        /// method to seed some initial data if there is no data in the database
        /// </summary>
        /// <param name="context"> dta context</param>
        public static void SeedEmployeeData(EmployeeContext context)
        {
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee
                     {
                        Name = "admin",
                        Phone = "123-456-7890",
                        Address = "123 Main St",
                        City = "Sample City",
                        Salary = 50000,
                        JobId = 1,
                        JoinedDate = DateTime.Now,
                        Password =BCrypt.Net.BCrypt.HashPassword("admin7890", 10)
                    },
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// method to seed some initial data if there is no data in the database
        /// </summary>
        /// <param name="context"> dta context</param>
        public static void SeedJobData(EmployeeContext context)
        {
            if (!context.Employees.Any())
            {

                var job = new Job[]
                {
                    new Job
                    {
                        JobTitle = "Manager",
                        DepartmentId = 1,
                    }
                };
                context.Jobs.AddRange(job);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// method to seed some initial data if there is no data in the database
        /// </summary>
        /// <param name="context"> dta context</param>
        public static void SeedDepartment(EmployeeContext context)
        {
            if(!context.Employees.Any())
            {
                var depts = new Department[]
                {
                    new Department
                    {
                        Name="Management"
                    }
                };
                context.Departments.AddRange(depts);
                context.SaveChanges();
            }
        }
    }
}
