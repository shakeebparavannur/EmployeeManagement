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

        /// <summary>
        /// This Method Return All employees that available in our data context
        /// </summary>
        /// <returns>List of Employees</returns>
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
        /// <summary>
        /// The method gives single employee by using the ID
        /// </summary>
        /// <param name="id">id of the employee</param>
        /// <returns>Employee Details</returns>
        [Authorize]
        [HttpGet("getemployee/{id}")]
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
        /// <summary>
        /// The method to create a new employee 
        /// Its authorized and authorized user with specific role can aceess this method
        /// </summary>
        /// <param name="employeeDTO">Essential datas needed to create an employee</param>
        /// <returns>A message saying sucess or failure</returns>
        [Authorize(Roles = "HR, Management")]
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDTO employeeDTO)
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
                bool ifEmailUnique =  employeeService.IsUniquePhonenumber(employeeDTO.Phone);
                if (!ifEmailUnique)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    response.ErrorMessages.Add("Phone number already exists");
                    return BadRequest(response);
                }


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
        /// <summary>
        /// The method used to login for the employee
        /// I have set an intial data to seed the database while its empty
        /// Check it out on Data=>DataSeeder.cs
        /// </summary>
        /// <param name="loginReq">phone number and password=>password should be
        /// Last 4 letter of name and last 4 digit of number</param>
        /// <returns>User data and JWT Token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> EmployeeLogin(LoginReqDTO loginReq)
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
        /// <summary>
        /// the method to delete an employee
        /// </summary>
        /// <param name="id">Id of the employee that needs to be deleted</param>
        /// <returns>return a bool value according to the status</returns>
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
        /// <summary>
        /// The method to update the employee data
        /// </summary>
        /// <param name="id">Id of the employee needs to be updated</param>
        /// <param name="employee"> Datas needed to be updated</param>
        /// <returns>the updated employee</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditEmployee(int id, CreateEmployeeDTO employee)
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
        /// <summary>
        /// The method to return the employees under a specific department
        /// </summary>
        /// <param name="id"> Id of the department needs to get</param>
        /// <returns> List of employees in the specific department</returns>
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
