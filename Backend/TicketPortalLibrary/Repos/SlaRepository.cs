using Microsoft.EntityFrameworkCore;
using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public class SlaRepository : ISlaRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task<SLA> CreateSlaAsync(SLA sla)
    {
        try
        {
            await _context.SLAs.AddAsync(sla);
            await _context.SaveChangesAsync();
            return sla;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while creating SLA. " + ex.Message);
        }
    }

    public async Task<SLA> UpdateSlaAsync(SLA sla)
    {
        var existing = await GetSlaByIdAsync(sla.SlaId);
        try
        {
            existing.ResponseTime = sla.ResponseTime;
            existing.ResolutionHours = sla.ResolutionHours;
            existing.Description = sla.Description;

            await _context.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while updating SLA. " + ex.Message);
        }
    }

    public async Task DeleteSlaAsync(int slaId)
    {
        var sla = await _context.SLAs
            .Include(s => s.TicketTypes)
            .FirstOrDefaultAsync(s => s.SlaId == slaId);

        if (sla == null)
        {
            throw new TicketException("SLA not found.");
        }
        else if (sla.TicketTypes.Count > 0)
        {
            throw new TicketException("SLA is associated with Ticket Types. Remove them before deleting SLA.");
        }

        _context.SLAs.Remove(sla);
        await _context.SaveChangesAsync();
    }

    public async Task<SLA?> GetSlaByIdAsync(int slaId)
    {
        var sla = await _context.SLAs.FirstOrDefaultAsync(s => s.SlaId == slaId);
        if (sla == null)
        {
            throw new TicketException("SLA not found.");
        }
        return sla;
    }

    public async Task<IEnumerable<SLA>> GetAllSlasAsync()
    {
        return await _context.SLAs.ToListAsync();
    }

    public async Task<SLA?> GetByTicketTypeIdAsync(int ticketTypeId)
    {
        var ticketType = await _context.TicketTypes
            .Include(tt => tt.SLA)
            .FirstOrDefaultAsync(tt => tt.TicketTypeId == ticketTypeId);

        if (ticketType == null)
        {
            throw new TicketException("Ticket Type not found.");
        }

        return ticketType.SLA;
    }
}
