using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace TicketPortalLibrary.Repos;

public class TicketRepository : ITicketRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateTicketAsync(Ticket ticket)
    {
        try
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TicketException("Unexpected error while creating ticket. " + ex.Message,499);
        }
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        Ticket existing =await GetTicketByIdAsync(ticket.TicketId);
        try
        {

            existing.Title= ticket.Title;
            existing.Description = ticket.Description;
            existing.Status = ticket.Status;
            existing.AssignedToEmpId = ticket.AssignedToEmpId;
            existing.ResolvedAt = ticket.ResolvedAt;
            existing.DueAt = ticket.DueAt;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TicketException("Unexpected error while updating ticket. " + ex.Message,499);
        }
    }

    public async Task DeleteTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets
                                .Include(t => t.TicketReplies)
                                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        if (ticket == null)
        {
            throw new TicketException("Ticket not found.",404);
        }
        else if (ticket.TicketReplies.Count > 0)
        {
            throw new TicketException("Ticket is being used.Delete All the Ticket logs Before Deleting",499);
        }
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<Ticket> GetTicketByIdAsync(int ticketId)
    {
        var ticketbyId=await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
        if (ticketbyId == null)
        {
            throw new TicketException("Ticket Not found",404);
        }
        return ticketbyId;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        var tickets=await _context.Tickets
                             .ToListAsync();
        return tickets;
    }

    public async Task<IEnumerable<Ticket>> GetByCreatedByEmpIdAsync(int empId)
    {
        var ticketbyCreatedEmpId=await _context.Tickets
                             .Where(t => t.CreatedByEmpId == empId)
                             .ToListAsync();
        return ticketbyCreatedEmpId;
    }

    public async Task<IEnumerable<Ticket>> GetByAssignedToEmpIdAsync(int empId)
    {
        var ticketbyAssignedEmpId=await _context.Tickets
                             .Where(t => t.AssignedToEmpId == empId)
                             .ToListAsync();
        return ticketbyAssignedEmpId;                     
    }

    public async Task<IEnumerable<Ticket>> GetByStatusAsync(string status)
    {
        var ticketbyStatus=await _context.Tickets
                             .Where(t => t.Status == status)
                             .ToListAsync();
        return ticketbyStatus;
    }

    public async Task<IEnumerable<Ticket>> GetByDepartmentIdAsync(int departmentId)
    {
        var ticketbyDepartmentId=await _context.Tickets
                             .Include(t => t.TicketType)
                             .Where(t => t.TicketType.DepartmentId == departmentId)
                             .ToListAsync();
        return ticketbyDepartmentId;
    }

    public async Task<IEnumerable<Ticket>> GetByDepartmentAndStatusAsync(int departmentId, string status)
    {
        var ticketByDepartmentAndStatus=await _context.Tickets.Include(t => t.TicketType)
                                    .Where(t => t.TicketType.DepartmentId == departmentId && t.Status == status)
                                    .ToListAsync();
        return ticketByDepartmentAndStatus;
    }

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(int ticketTypeId)
    {
        var ticketsByTypeId=await _context.Tickets.Where(t => t.TicketTypeId == ticketTypeId).ToListAsync();
        return ticketsByTypeId;
    }

    public async Task<IEnumerable<Ticket>> GetOverdueTicketsAsync()
    {
        var overdueTickets=await _context.Tickets.Where(t => t.ResolvedAt == null && DateTime.UtcNow > t.DueAt).ToListAsync();
        return overdueTickets;
    }
}
