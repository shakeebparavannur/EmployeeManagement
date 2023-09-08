using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Service
{
    public class JobService : IJobService

    {
        private readonly EmployeeContext _employeeContext;
        private readonly IMapper mapper;
        public JobService(EmployeeContext employeeContext,IMapper mapper)
        {
            _employeeContext = employeeContext;
            this.mapper = mapper;
            
        }
        public async Task<Job> AddNewJob(JobDTO job)
        {
            if(job == null)
            {
                throw new ArgumentNullException("Please enter some valid data");
            }
           var jobDto= mapper.Map<Job>(job);
            await _employeeContext.Jobs.AddAsync(jobDto);
            await _employeeContext.SaveChangesAsync();
            return jobDto;
        }

        public Task<IEnumerable<Job>> GetAllJob()
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveJob(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Job> UpdateJob(int id, JobDTO job)
        {
            throw new NotImplementedException();
        }
    }
}
