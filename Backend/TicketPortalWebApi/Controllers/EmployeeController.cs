using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{empId}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(int empId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("department/{departmentId}")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetByDepartment(int departmentId)
        {
            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            return Ok(employees);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                await _employeeRepository.CreateEmployeeAsync(employee);
                return Created($"api/employee/{employee.EmpId}", employee);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 404)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{empId}")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int empId)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(empId);
                return Ok("Employee deleted successfully");
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 404)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Login(string email, string password)
        {
            try
            {
                var employee = await _employeeRepository.LoginEmployee(email, password);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
