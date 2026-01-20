using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("createdby/{empId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetByCreatedBy(string empId)
        {
            var tickets = await _ticketRepository.GetByCreatedByEmpIdAsync(empId);
            return Ok(tickets);
        }

        [HttpGet("assignedto/{empId}")]
        public async Task<ActionResult> GetByAssignedTo(string empId)
        {
            var tickets = await _ticketRepository.GetByAssignedToEmpIdAsync(empId);
            return Ok(tickets);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult> GetByStatus(string status)
        {
            var tickets = await _ticketRepository.GetByStatusAsync(status);
            return Ok(tickets);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult> GetByDepartment(string departmentId)
        {
            var tickets = await _ticketRepository.GetByDepartmentIdAsync(departmentId);
            return Ok(tickets);
        }

        [HttpGet("departmentwithstatus/{departmentId}/{status}")]
        public async Task<ActionResult> GetByDepartmentAndStatus(string departmentId, string status)
        {
            var tickets = await _ticketRepository
                .GetByDepartmentAndStatusAsync(departmentId, status);
            return Ok(tickets);
        }

        [HttpGet("byTickettype/{ticketTypeId}")]
        public async Task<ActionResult> GetByTicketType(string ticketTypeId)
        {
            var tickets = await _ticketRepository.GetByTicketTypeIdAsync(ticketTypeId);
            return Ok(tickets);
        }

        [HttpGet("overdue")]
        public async Task<ActionResult> GetOverdue()
        {
            var tickets = await _ticketRepository.GetOverdueTicketsAsync();
            return Ok(tickets);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            try
            {
                await _ticketRepository.CreateTicketAsync(ticket);
                return Created($"api/ticket/{ticket.TicketId}", ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicketAsync(ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                if(ex.ErrorNumber==404)
                {
                    return NotFound(ex.Message);
                }
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int ticketId)
        {
            try
            {
                await _ticketRepository.DeleteTicketAsync(ticketId);
                return Ok("Ticket deleted successfully");
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
