using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private APIResponse response;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            response = new();
        }
        [HttpGet("getemployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await employeeService.GetAllEmployees();
                if (employees == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = employees;
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
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error occured while fetching data");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = employee;
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
        [Authorize(Roles ="HR, Management")]
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDTO employeeDTO)
        {
            try
            {


                var addEmployee = await employeeService.AddNewEmployee(employeeDTO);
                if (addEmployee == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("somthing went wrong");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = addEmployee;
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
        [HttpPost("login")]
        public async Task<IActionResult> EmployeeLogin(LoginReqDTO loginReq)
        {
            try
            {
                var loginRes = await employeeService.Login(loginReq);
                if (loginRes == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("invalid credential");
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = loginRes;
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
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = await employeeService.DeleteEmployeeById(id);
                if (deleteEmployee == false)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Not Found");
                    response.IsSuccess = false;
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = deleteEmployee;
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
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditEmployee(int id, CreateEmployeeDTO employee)
        {
            try
            {


                var editEmployee = await employeeService.UpdateEmployeeById(id, employee);
                if (editEmployee == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = editEmployee;
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
        [HttpGet("department/{id}")]
        public async Task<IActionResult> GetEmployeesByDepartment(int id)
        {
            try
            {


                var employees = await employeeService.GetEmployeesByDept(id);
                if (employees == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Something Went Wrong");
                    response.IsSuccess = false;
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = employees;
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
