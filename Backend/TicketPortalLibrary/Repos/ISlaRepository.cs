using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ISlaRepository{
    Task CreateSlaAsync(SLA sla);
    Task UpdateSlaAsync(SLA sla);
    Task DeleteSlaAsync(string slaId);
    Task<SLA?> GetSlaByIdAsync(string slaId);
    Task<IEnumerable<SLA>> GetAllSlasAsync();
}
