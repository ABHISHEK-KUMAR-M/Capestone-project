using Microsoft.EntityFrameworkCore;

namespace TicketPortalLibrary.Models;

public class TicketPortalDbContext : DbContext
{
    public TicketPortalDbContext()
    {
    }

    public TicketPortalDbContext(DbContextOptions<TicketPortalDbContext> options)
        : base(options)
    {
    }

    /* DbSets */
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<SLA> SLAs { get; set; }
    public virtual DbSet<TicketType> TicketTypes { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<TicketReply> TicketReplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "data source=localhost\\SQLEXPRESS; database=TicketPortalDB; integrated security=true; Trust Server Certificate=true"
        );
    }
}