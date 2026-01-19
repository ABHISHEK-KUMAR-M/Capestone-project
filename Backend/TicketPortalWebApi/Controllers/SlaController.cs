using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketPortalLibrary.Models;
using TicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlaController : ControllerBase
    {
        private readonly ISlaRepository _slaRepository;

        public SlaController(ISlaRepository slaRepository)
        {
            _slaRepository = slaRepository;
        }

        // GET: api/sla
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var slas = await _slaRepository.GetAllSlasAsync();
            return Ok(slas);
        }

        // GET: api/sla/5
        [HttpGet("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(int slaId)
        {
            try
            {
                var sla = await _slaRepository.GetSlaByIdAsync(slaId);
                return Ok(sla);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/sla/bytickettype/3
        [HttpGet("bytickettype/{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicketType(int ticketTypeId)
        {
            try
            {
                var sla = await _slaRepository.GetByTicketTypeIdAsync(ticketTypeId);
                return Ok(sla);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/sla
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Create(SLA sla)
        {
            try
            {
                await _slaRepository.CreateSlaAsync(sla);
                return Created($"api/sla/{sla.SlaId}", sla);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/sla
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(SLA sla)
        {
            try
            {
                await _slaRepository.UpdateSlaAsync(sla);
                return Ok(sla);
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

        // DELETE: api/sla/5
        [HttpDelete("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int slaId)
        {
            try
            {
                await _slaRepository.DeleteSlaAsync(slaId);
                return Ok("SLA deleted successfully");
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
