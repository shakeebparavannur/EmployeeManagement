using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Service
{
    public interface IJobService
    {
        Task<Job> AddNewJob(JobDTO job);
        Task<bool> RemoveJob(int id);
        Task<Job> UpdateJob(int id, JobDTO job);
        Task<IEnumerable<Job>> GetAllJob();
        Task<Job> GetJobById(int id);

    }
}
