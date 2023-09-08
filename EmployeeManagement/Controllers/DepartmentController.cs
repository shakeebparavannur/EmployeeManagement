using Azure;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private APIResponse response;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            response = new();
        }
        /// <summary>
        /// Method to get all department
        /// </summary>
        /// <returns>List of Department</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDept()
        {
            try
            {
                var getDept = await _departmentService.GetAllDepartment();
                if(getDept == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = getDept;
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
        /// Method to get the department by id
        /// </summary>
        /// <param name="id">Id of the departmet that we need</param>
        /// <returns>Department details with the id</returns>
        [HttpGet("departmet/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id) 
        {
            try
            {
                var dept = await _departmentService.GetDepartmentById(id);
                if(dept == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = dept;
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
        /// <summary>
        /// Method to add new department
        /// </summary>
        /// <param name="department"> Datas about the new department</param>
        /// <returns> New department</returns>
        [HttpPost("add-new-department")]
        public async Task<IActionResult> AddDepartmet(DepartmentDTO department)
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
                var dept = await _departmentService.AddNewDepartment(department);
                if(dept == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = dept;
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
        /// <summary>
        /// Method to delete a department
        /// </summary>
        /// <param name="id">Id of the department to be deleted</param>
        /// <returns>True or false by the deleted status</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            try
            {
                var delDept = await _departmentService.RemoveDepartment(id);
                if (delDept == false)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Unable to delete the data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = delDept;
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
        /// <summary>
        /// Method to update the existing department
        /// </summary>
        /// <param name="id">Id of the department needs to update</param>
        /// <param name="department"> Datas need to update</param>
        /// <returns>Updated Department</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id,DepartmentDTO department)
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
                var updateDept = await _departmentService.UpdateDepartment(id, department);
                if (updateDept == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("unable to update the data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = updateDept;
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
