using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketReplyController : ControllerBase
    {
        private readonly ITicketReplyRepository _ticketReplyRepository;

        public TicketReplyController(ITicketReplyRepository ticketReplyRepository)
        {
            _ticketReplyRepository = ticketReplyRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var replies = await _ticketReplyRepository.GetAllTicketRepliesAsync();
            return Ok(replies);
        }

        [HttpGet("{replyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(int replyId)
        {
            try
            {
                var reply = await _ticketReplyRepository.GetTicketReplyByIdAsync(replyId);
                return Ok(reply);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ticket/{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicket(int ticketId)
        {
            try{    
                var replies = await _ticketReplyRepository.GetByTicketIdAsync(ticketId);
                return Ok(replies);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("employee/{empId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByEmployee(string empId)
        {
            try{    
                var replies = await _ticketReplyRepository.GetByEmployeeIdAsync(empId);
                return Ok(replies);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(TicketReply reply)
        {
            try
            {
                await _ticketReplyRepository.CreateTicketReplyAsync(reply);
                return Created($"api/ticketreply/{reply.ReplyId}", reply);
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
        public async Task<ActionResult> Update(TicketReply reply)
        {
            try
            {
                await _ticketReplyRepository.UpdateTicketReplyAsync(reply);
                return Ok(reply);
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

        [HttpDelete("{replyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int replyId)
        {
            try
            {
                await _ticketReplyRepository.DeleteTicketReplyAsync(replyId);
                return Ok("Ticket reply deleted successfully");
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
