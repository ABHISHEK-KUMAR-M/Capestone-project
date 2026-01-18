using TicketPortalLibrary.Models;

namespace TicketPortalLibrary.Repos;

public interface ITicketReplyRepository
{
    Task<TicketReply> CreateTicketReplyAsync(TicketReply reply);
    Task<TicketReply> UpdateTicketReplyAsync(TicketReply reply);
    Task DeleteTicketReplyAsync(int replyId);
    Task<TicketReply?> GetTicketReplyByIdAsync(int replyId);
    Task<IEnumerable<TicketReply>> GetAllTicketRepliesAsync();
    Task<IEnumerable<TicketReply>> GetByTicketIdAsync(int ticketId);
    Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(int empId);
}
