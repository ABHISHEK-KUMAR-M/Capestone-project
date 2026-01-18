using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ITicketTypeRepository
{
    Task<TicketType> CreateTicketTypeAsync(TicketType ticketType);
    Task<TicketType> UpdateTicketTypeAsync(TicketType ticketType);
    Task DeleteTicketTypeAsync(int ticketTypeId);
    Task<TicketType?> GetTicketTypeByIdAsync(int ticketTypeId);
    Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
    Task<IEnumerable<TicketType>> GetByDepartmentIdAsync(int departmentId);
    Task<IEnumerable<TicketType>> GetBySlaIdAsync(int SlaId);
}
