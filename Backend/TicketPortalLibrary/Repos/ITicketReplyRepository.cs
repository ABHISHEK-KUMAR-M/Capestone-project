using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ITicketReplyRepository
{
    Task CreateTicketReplyAsync(TicketReply reply);
    Task UpdateTicketReplyAsync(int ticketReplyId,TicketReply reply);
    Task DeleteTicketReplyAsync(int replyId);
    Task<TicketReply?> GetTicketReplyByIdAsync(int replyId);
    Task<IEnumerable<TicketReply>> GetAllTicketRepliesAsync();
    Task<IEnumerable<TicketReply>> GetByTicketIdAsync(int ticketId);
    Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(string empId);
}
