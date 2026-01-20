using TicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace TicketPortalLibrary.Repos;

public class TicketTypeRepository : ITicketTypeRepository{
    private readonly TicketPortalDbContext context = new();
    public async Task CreateTicketTypeAsync(TicketType ticketType){
        try{
            await context.TicketTypes.AddAsync(ticketType);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex){
            throw new TicketException("Unexpected error while creating ticket type. " + ex.Message, 499);
        }
    }
    public async Task UpdateTicketTypeAsync(TicketType ticketType){
        TicketType? existing = await GetTicketTypeByIdAsync(ticketType.TicketTypeId);
        try{
            if (existing == null){
                throw new TicketException("Ticket type not found.", 404);
            }
            existing.TypeName = ticketType.TypeName;
            existing.Description = ticketType.Description;
            existing.DepartmentId = ticketType.DepartmentId;
            existing.SlaId = ticketType.SlaId;
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
        catch (Exception ex){
            throw new TicketException("Unexpected error while updating ticket type. " + ex.Message, 499);
        }
    }
    public async Task DeleteTicketTypeAsync(string ticketTypeId){
        var ticketType = await context.TicketTypes.Include(tt => tt.Tickets).FirstOrDefaultAsync(tt => tt.TicketTypeId == ticketTypeId);
        if (ticketType == null){
            throw new TicketException("Ticket type not found.", 404);
        }
        else if (ticketType.Tickets.Count > 0){
            throw new TicketException("Ticket type is being used. Delete related tickets before deleting.", 499);
        }
        try{    
            context.TicketTypes.Remove(ticketType);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw SqlExceptionMapper.Map(sqlEx);
        }
    }
    public async Task<TicketType?> GetTicketTypeByIdAsync(string ticketTypeId){
        var ticketType = await context.TicketTypes.Include(tt => tt.Department).Include(tt => tt.SLA).FirstOrDefaultAsync(tt => tt.TicketTypeId == ticketTypeId);
        if (ticketType == null){
            throw new TicketException("Ticket type not found.", 404);
        }
        return ticketType;
    }
    public async Task<IEnumerable<TicketType>> GetAllTicketTypesAsync(){
        var ticketTypes = await context.TicketTypes.Include(tt => tt.Department).Include(tt => tt.SLA).ToListAsync();
        return ticketTypes;
    }
    public async Task<IEnumerable<TicketType>> GetByDepartmentIdAsync(string departmentId){
        var ticketTypesByDepartment = await context.TicketTypes.Where(tt => tt.DepartmentId == departmentId).Include(tt => tt.Department).Include(tt => tt.SLA).ToListAsync();
        return ticketTypesByDepartment;
    }
    public async Task<IEnumerable<TicketType>> GetBySlaIdAsync(string slaId){
        var ticketTypesBySla = await context.TicketTypes.Where(tt =>tt.SlaId==slaId).Include(tt => tt.Department).Include(tt => tt.SLA).ToListAsync();
        return ticketTypesBySla;
    }
}
