using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Job>> GetAllJob()
        {
            var jobs = await _employeeContext.Jobs.ToListAsync();
            if (jobs == null)
            {
                return null;
            }
            return jobs;
        }
        public async Task<Job> GetJobById(int id)
        {
            var job = await _employeeContext.Jobs.SingleOrDefaultAsync(j=>j.JobId == id);
            if (job == null)
            {
                throw new Exception("Data not found");
            }
            return job;
        }

        public async Task<bool> RemoveJob(int id)
        {
            var job = await _employeeContext.Jobs.SingleOrDefaultAsync(j=> j.JobId == id);
            if(job == null)
            {
                return false;
            }
            _employeeContext.Jobs.Remove(job);
            await _employeeContext.SaveChangesAsync() ;
            return true;
        }

        public async Task<Job> UpdateJob(int id, JobDTO job)
        {
            var _job = await _employeeContext.Jobs.SingleOrDefaultAsync(j => j.JobId == id);
            if(_job == null)
            {
                throw new Exception("Data Not Found");
            }
            mapper.Map(job,_job);
            await _employeeContext.SaveChangesAsync();
            return _job;
        }
    }
}
