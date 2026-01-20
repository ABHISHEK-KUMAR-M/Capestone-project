using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while creating ticket. {ex.Message}",499);
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
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException($"Unexpected error while creating ticket. {ex.Message}",499);
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
        try{    
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
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

    public async Task<IEnumerable<Ticket>> GetByCreatedByEmpIdAsync(string empId)
    {
        var ticketbyCreatedEmpId=await _context.Tickets
                             .Where(t => t.CreatedByEmpId == empId)
                             .ToListAsync();
        if (ticketbyCreatedEmpId.Count==0)
        {
            throw new TicketException("No Ticket were found from this Employee.",404);
        }
        return ticketbyCreatedEmpId;
    }

    public async Task<IEnumerable<Ticket>> GetByAssignedToEmpIdAsync(string empId)
    {
        var ticketbyAssignedEmpId=await _context.Tickets
                             .Where(t => t.AssignedToEmpId == empId)
                             .ToListAsync();
        if (ticketbyAssignedEmpId.Count==0)
        {
            throw new TicketException("No Ticket were found for this Employee.",404);
        }                            
        return ticketbyAssignedEmpId;                     
    }

    public async Task<IEnumerable<Ticket>> GetByStatusAsync(string status)
    {
        var ticketbyStatus=await _context.Tickets
                             .Where(t => t.Status == status)
                             .ToListAsync();

        if (ticketbyStatus.Count==0)
        {
            throw new TicketException("No Ticket were found for this Employee.",404);
        }   
        return ticketbyStatus;
    }

    public async Task<IEnumerable<Ticket>> GetByDepartmentIdAsync(string departmentId)
    {
        var ticketbyDepartmentId=await _context.Tickets
                             .Include(t => t.TicketType)
                             .Where(t => t.TicketType.DepartmentId == departmentId)
                             .ToListAsync();
        if (ticketbyDepartmentId.Count==0)
        {
            throw new TicketException("No Ticket were found for this Department.",404);
        } 
        return ticketbyDepartmentId;
    }

    public async Task<IEnumerable<Ticket>> GetByDepartmentAndStatusAsync(string departmentId, string status)
    {
        var ticketByDepartmentAndStatus=await _context.Tickets.Include(t => t.TicketType)
                                    .Where(t => t.TicketType.DepartmentId == departmentId && t.Status == status)
                                    .ToListAsync();
        if (ticketByDepartmentAndStatus.Count==0)
        {
            throw new TicketException("No Ticket were found for this Department along with this status code.",404);
        } 
        return ticketByDepartmentAndStatus;
    }

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(string ticketTypeId)
    {
        var ticketsByTypeId=await _context.Tickets.Where(t => t.TicketTypeId == ticketTypeId).ToListAsync();
        if (ticketsByTypeId.Count==0)
        {
            throw new TicketException("No Ticket were found for this TicketType.",404);
        } 
        return ticketsByTypeId;
    }

    public async Task<IEnumerable<Ticket>> GetOverdueTicketsAsync()
    {
        var overdueTickets=await _context.Tickets.Where(t => t.ResolvedAt == null && DateTime.UtcNow > t.DueAt).ToListAsync();
        if (overdueTickets.Count==0)
        {
            throw new TicketException("No Ticket were found for this TicketType.",404);
        } 
        return overdueTickets;
    }
}
