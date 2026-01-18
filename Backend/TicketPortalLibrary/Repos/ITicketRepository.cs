using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;
public interface ITicketRepository
{
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task<Ticket> UpdateTicketAsync(Ticket ticket);
    
    Task DeleteTicketAsync(int ticketId);
    Task<Ticket?> GetTicketByIdAsync(int ticketId);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetByCreatedByEmpIdAsync(int empId);
    Task<IEnumerable<Ticket>> GetByAssignedToEmpIdAsync(int empId);
    Task<IEnumerable<Ticket>> GetByStatusAsync(string status);
    Task<IEnumerable<Ticket>> GetByDepartmentIdAsync(int departmentId);
    Task<IEnumerable<Ticket>> GetByDepartmentAndStatusAsync(int departmentId,string status);
    Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(int ticketTypeId);
    Task<IEnumerable<Ticket>> GetOverdueTicketsAsync();
}
