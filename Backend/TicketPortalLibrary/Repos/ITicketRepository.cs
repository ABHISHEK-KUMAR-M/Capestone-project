using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;
public interface ITicketRepository
{
    Task CreateTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);
    
    Task DeleteTicketAsync(int ticketId);
    Task<Ticket> GetTicketByIdAsync(int ticketId);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetByCreatedByEmpIdAsync(string empId);
    Task<IEnumerable<Ticket>> GetByAssignedToEmpIdAsync(string empId);
    Task<IEnumerable<Ticket>> GetByStatusAsync(string status);
    Task<IEnumerable<Ticket>> GetByDepartmentIdAsync(string departmentId);
    Task<IEnumerable<Ticket>> GetByDepartmentAndStatusAsync(string departmentId,string status);
    Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(string ticketTypeId);
    Task<IEnumerable<Ticket>> GetOverdueTicketsAsync();
}
