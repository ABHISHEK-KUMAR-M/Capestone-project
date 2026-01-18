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
            "Server=tcp:capestone-team2.database.windows.net,1433;Initial Catalog=TicketPortal;Persist Security Info=False;User ID=Abhishek;Password=Aryansh@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedByEmployee)
            .WithMany(e => e.CreatedTickets)
            .HasForeignKey(t => t.CreatedByEmpId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedEmployee)
            .WithMany(e => e.AssignedTickets)
            .HasForeignKey(t => t.AssignedToEmpId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}