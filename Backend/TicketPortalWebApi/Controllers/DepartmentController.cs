using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(string departmentId)
        {
            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
                return Ok(department);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(Department department)
        {
            try
            {
                await _departmentRepository.CreateDepartmentAsync(department);
                return Created($"api/department/{department.DepartmentId}", department);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string departmentId,Department department)
        {
            try
            {
                await _departmentRepository.UpdateDepartmentAsync(departmentId,department);
                return Ok(department);
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

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string departmentId)
        {
            try
            {
                await _departmentRepository.DeleteDepartmentAsync(departmentId);
                return Ok("Department deleted successfully");
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
    }
}
