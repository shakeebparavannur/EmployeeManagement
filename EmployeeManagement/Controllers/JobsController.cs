using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService jobService;
        private APIResponse response;
        public JobsController(IJobService jobService)
        {
            this.jobService = jobService;
            response = new APIResponse();
        }
        [HttpPost("add-new-job")]
        public async Task<IActionResult> AddNewJob(JobDTO job)
        {
            try
            {


                var newJob = await jobService.AddNewJob(job);
                if (newJob == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = newJob;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
    }
}
