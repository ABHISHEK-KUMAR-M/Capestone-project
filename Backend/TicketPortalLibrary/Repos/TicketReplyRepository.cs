using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketPortalLibrary.Repos;

public class TicketReplyRepository : ITicketReplyRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task<TicketReply> CreateTicketReplyAsync(TicketReply reply)
    {
        try
        {
            reply.CreatedAt = DateTime.UtcNow;
            await _context.TicketReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
            return reply;
        }
        catch (Exception ex)
        {
            throw new TicketException(
                "Unexpected error while creating ticket reply. " + ex.Message,
                499
            );
        }
    }

    public async Task<TicketReply> UpdateTicketReplyAsync(TicketReply reply)
    {
        TicketReply existing = await GetTicketReplyByIdAsync(reply.ReplyId);

        try
        {
            existing.Message = reply.Message;
            await _context.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new TicketException(
                "Unexpected error while updating ticket reply. " + ex.Message,
                499
            );
        }
    }

    public async Task DeleteTicketReplyAsync(int replyId)
    {
        var reply = await _context.TicketReplies
                                  .FirstOrDefaultAsync(r => r.ReplyId == replyId);

        if (reply == null)
        {
            throw new TicketException("Ticket reply not found.", 404);
        }

        _context.TicketReplies.Remove(reply);
        await _context.SaveChangesAsync();
    }

    public async Task<TicketReply?> GetTicketReplyByIdAsync(int replyId)
    {
        var replyById = await _context.TicketReplies
                                      .FirstOrDefaultAsync(r => r.ReplyId == replyId);

        if (replyById == null)
        {
            throw new TicketException("Ticket reply not found.", 404);
        }

        return replyById;
    }

    public async Task<IEnumerable<TicketReply>> GetAllTicketRepliesAsync()
    {
        var replies = await _context.TicketReplies
                                    .OrderByDescending(r => r.CreatedAt)
                                    .ToListAsync();
        return replies;
    }

    public async Task<IEnumerable<TicketReply>> GetByTicketIdAsync(int ticketId)
    {
        var repliesByTicketId = await _context.TicketReplies
                                              .Where(r => r.TicketId == ticketId)
                                              .OrderBy(r => r.CreatedAt)
                                              .ToListAsync();
        return repliesByTicketId;
    }

    public async Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(int empId)
    {
        var repliesByEmployee = await _context.TicketReplies
            .Where(r =>
                r.RepliedByCreatorEmpId == empId ||
                r.RepliedByAssignedEmpId == empId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return repliesByEmployee;
    }
}
