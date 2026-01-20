using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        public TicketTypeController(ITicketTypeRepository ticketTypeRepository){
            _ticketTypeRepository = ticketTypeRepository;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll(){
            var ticketTypes = await _ticketTypeRepository.GetAllTicketTypesAsync();
            return Ok(ticketTypes);
        }
        [HttpGet("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(string ticketTypeId){
            try{
                var ticketType = await _ticketTypeRepository.GetTicketTypeByIdAsync(ticketTypeId);
                return Ok(ticketType);
            }
            catch (TicketException ex){
                return NotFound(ex.Message);
            }
        }
        [HttpGet("department/{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByDepartment(string departmentId){
            try{    
                var ticketTypes = await _ticketTypeRepository.GetByDepartmentIdAsync(departmentId);
                return Ok(ticketTypes);
            }
            catch (TicketException ex){
                return NotFound(ex.Message);
            }
        }
        [HttpGet("sla/{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetBySla(string slaId){
            try{    
                var ticketTypes = await _ticketTypeRepository.GetBySlaIdAsync(slaId);
                return Ok(ticketTypes);
            }
            catch (TicketException ex){
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(TicketType ticketType){
            try{
                await _ticketTypeRepository.CreateTicketTypeAsync(ticketType);
                return Created($"api/tickettype/{ticketType.TicketTypeId}", ticketType);
            }
            catch (TicketException ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(TicketType ticketType){
            try{
                await _ticketTypeRepository.UpdateTicketTypeAsync(ticketType);
                return Ok(ticketType);
            }
            catch (TicketException ex){
                if (ex.ErrorNumber == 404){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string ticketTypeId){
            try{
                await _ticketTypeRepository.DeleteTicketTypeAsync(ticketTypeId);
                return Ok("Ticket type deleted successfully");
            }
            catch (TicketException ex){
                if (ex.ErrorNumber == 404){
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }
    }
}
