using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TicketPortalLibrary.Repos;

public class TicketReplyRepository : ITicketReplyRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateTicketReplyAsync(TicketReply reply)
    {
        try
        {
            reply.CreatedAt = DateTime.UtcNow;
            await _context.TicketReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex)
        {
            throw new TicketException(
                "Unexpected error while creating ticket reply. " + ex.Message,
                499
            );
        }
    }

    public async Task UpdateTicketReplyAsync(TicketReply reply)
    {
        TicketReply existing = await GetTicketReplyByIdAsync(reply.ReplyId);

        try
        {
            existing.Message = reply.Message;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
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

        try{   
            _context.TicketReplies.Remove(reply);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
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
        if (repliesByTicketId.Count==0)
        {
            throw new TicketException("Ticket Reply not found for this ticket ID.",404);
        }
        return repliesByTicketId;
    }

    public async Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(string empId)
    {
        var repliesByEmployee = await _context.TicketReplies
            .Where(r =>
                r.RepliedByCreatorEmpId == empId ||
                r.RepliedByAssignedEmpId == empId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
        if (repliesByEmployee.Count==0)
        {
            throw new TicketException("Ticket Reply not found for this Employee.",404);
        }

        return repliesByEmployee;
    }
}
