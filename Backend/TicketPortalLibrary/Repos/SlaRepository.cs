using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public class SlaRepository : ISlaRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateSlaAsync(SLA sla)
    {
        try
        {
            await _context.SLAs.AddAsync(sla);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while updating SLA. {ex.Message}",499);
        }
    }

    public async Task UpdateSlaAsync(SLA sla)
    {
        var existing = await GetSlaByIdAsync(sla.SlaId);
        try
        {
            existing.ResponseTime = sla.ResponseTime;
            existing.ResolutionHours = sla.ResolutionHours;
            existing.Description = sla.Description;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while updating SLA. {ex.Message}",499);
        }
    }

    public async Task DeleteSlaAsync(string slaId)
    {
        var sla = await _context.SLAs
            .Include(s => s.TicketTypes)
            .FirstOrDefaultAsync(s => s.SlaId == slaId);

        if (sla == null)
        {
            throw new TicketException("SLA not found.",404);
        }
        else if (sla.TicketTypes.Count > 0)
        {
            throw new TicketException("SLA is associated with Ticket Types. Remove them before deleting SLA.",499);
        }

        _context.SLAs.Remove(sla);
        await _context.SaveChangesAsync();
    }

    public async Task<SLA?> GetSlaByIdAsync(string slaId)
    {
        var sla = await _context.SLAs.FirstOrDefaultAsync(s => s.SlaId == slaId);
        if (sla == null)
        {
            throw new TicketException("SLA not found.",404);
        }
        return sla;
    }

    public async Task<IEnumerable<SLA>> GetAllSlasAsync()
    {
        return await _context.SLAs.ToListAsync();
    }

}
