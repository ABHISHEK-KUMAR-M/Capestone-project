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

        [HttpGet("empId/{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByEmpId(string empId)
        {
            try{    
                var tickets = await _ticketRepository.GetByEmpIdAsync(empId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByStatus(string status)
        {
            try{    
                var tickets = await _ticketRepository.GetByStatusAsync(status);
                return Ok(tickets);
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
                var tickets = await _ticketRepository.GetByDepartmentIdAsync(departmentId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("departmentwithstatus/{departmentId}/{status}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByDepartmentAndStatus(string departmentId, string status)
        {
            try{    
                var tickets = await _ticketRepository
                    .GetByDepartmentAndStatusAsync(departmentId, status);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }   
        }

        [HttpGet("byTickettype/{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicketType(string ticketTypeId)
        {
            try{    
                var tickets = await _ticketRepository.GetByTicketTypeIdAsync(ticketTypeId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("overdue")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOverdue()
        {
            try{    
                var tickets = await _ticketRepository.GetOverdueTicketsAsync();
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
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

        [HttpPut("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int ticketId,Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicketAsync(ticketId,ticket);
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
