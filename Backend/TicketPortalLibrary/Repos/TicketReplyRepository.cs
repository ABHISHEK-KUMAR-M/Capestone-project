using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
=======
using Microsoft.Data.SqlClient;
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265

namespace TicketPortalLibrary.Repos;

public class TicketReplyRepository : ITicketReplyRepository
{
    private readonly TicketPortalDbContext _context = new();

<<<<<<< HEAD
    public async Task<TicketReply> CreateTicketReplyAsync(TicketReply reply)
=======
    public async Task CreateTicketReplyAsync(TicketReply reply)
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
    {
        try
        {
            reply.CreatedAt = DateTime.UtcNow;
            await _context.TicketReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
<<<<<<< HEAD
            return reply;
=======
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
        }
        catch (Exception ex)
        {
            throw new TicketException(
                "Unexpected error while creating ticket reply. " + ex.Message,
                499
            );
        }
    }

<<<<<<< HEAD
    public async Task<TicketReply> UpdateTicketReplyAsync(TicketReply reply)
=======
    public async Task UpdateTicketReplyAsync(TicketReply reply)
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
    {
        TicketReply existing = await GetTicketReplyByIdAsync(reply.ReplyId);

        try
        {
            existing.Message = reply.Message;
            await _context.SaveChangesAsync();
<<<<<<< HEAD
            return existing;
=======
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
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

<<<<<<< HEAD
        _context.TicketReplies.Remove(reply);
        await _context.SaveChangesAsync();
=======
        try{   
            _context.TicketReplies.Remove(reply);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
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

<<<<<<< HEAD
    public async Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(int empId)
=======
    public async Task<IEnumerable<TicketReply>> GetByEmployeeIdAsync(string empId)
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
    {
        var repliesByEmployee = await _context.TicketReplies
            .Where(r =>
                r.RepliedByCreatorEmpId == empId ||
                r.RepliedByAssignedEmpId == empId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return repliesByEmployee;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> abe0cf733e02927bdb6b3c880ec989a7c56fc265
