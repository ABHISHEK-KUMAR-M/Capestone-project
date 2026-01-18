using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ISlaRepository{
    Task<SLA> CreateSlaAsync(SLA sla);
    Task<SLA> UpdateSlaAsync(SLA sla);
    Task DeleteSlaAsync(int slaId);
    Task<SLA?> GetSlaByIdAsync(int slaId);
    Task<IEnumerable<SLA>> GetAllSlasAsync();
    Task<SLA?> GetByTicketTypeIdAsync(int ticketTypeId);
}
