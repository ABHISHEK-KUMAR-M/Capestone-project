using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ITicketTypeRepository
{
    Task CreateTicketTypeAsync(TicketType ticketType);
    Task UpdateTicketTypeAsync(string ticketTypeId,TicketType ticketType);
    Task DeleteTicketTypeAsync(string ticketTypeId);
    Task<TicketType?> GetTicketTypeByIdAsync(string ticketTypeId);
    Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
    Task<IEnumerable<TicketType>> GetByDepartmentIdAsync(string departmentId);
    Task<IEnumerable<TicketType>> GetBySlaIdAsync(string SlaId);
}
