﻿using EmployeeManagement.Models;
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
        /// <summary>
        /// Method to create a new job
        /// </summary>
        /// <param name="job"> details aboout the job</param>
        /// <returns> Added Job Datas</returns>
        [HttpPost("add-new-job")]
        public async Task<IActionResult> AddNewJob(JobDTO job)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.AddRange(validationErrors);
                response.IsSuccess = false;
                return BadRequest(response);
            }
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
        /// <summary>
        /// Method to get all jobs in the data context
        /// </summary>
        /// <returns> List of Jobs</returns>
        [HttpGet("getalljobs")]
        public async Task<IActionResult> GetJobs()
        {
            try
            {
                var jobs = await jobService.GetAllJob();
                if(jobs == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = jobs;
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
        /// <summary>
        /// The methods to return job ny id
        /// </summary>
        /// <param name="id">Id of the job</param>
        /// <returns> The job witn the id</returns>
        [HttpGet("job/{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            try
            {
                var job = await jobService.GetJobById(id);
                if(job == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = job;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemoveJob(int id)
        {
            try
            {
                var removeJob = await jobService.RemoveJob(id);
                if (removeJob == false)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Unable to delete the data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = removeJob;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJob(int id, JobDTO job)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.AddRange(validationErrors);
                response.IsSuccess = false;
                return BadRequest(response);
            }
            try
            {
                var updateJob = await jobService.UpdateJob(id, job);
                if (updateJob == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("unable to update the data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = updateJob;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add(ex.Message);
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
    }
}
