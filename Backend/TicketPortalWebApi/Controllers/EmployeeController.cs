using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(string empId)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByDepartment(string departmentId)
        {
            try{    
                var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
                return Ok(employees);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
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

        [HttpPut("{employeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string employeeId,Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employeeId,employee);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string empId)
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

        [HttpGet("login/{empId}/{password}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Login(string empId, string password)
        {
            try
            {
                var employee = await _employeeRepository.LoginEmployee(empId, password);
                return Ok(employee);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
