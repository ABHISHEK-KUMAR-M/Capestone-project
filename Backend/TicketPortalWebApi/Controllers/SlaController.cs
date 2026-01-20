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


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            var slas = await _slaRepository.GetAllSlasAsync();
            return Ok(slas);
        }


        [HttpGet("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetById(string slaId)
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


        [HttpPut("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string slaId,SLA sla)
        {
            try
            {
                await _slaRepository.UpdateSlaAsync(slaId,sla);
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

       
        [HttpDelete("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string slaId)
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
