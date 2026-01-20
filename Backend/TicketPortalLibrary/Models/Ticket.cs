using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
    [Column(TypeName = "varchar(150)")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
    [Column(TypeName = "varchar(2000)")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Ticket type is required")]
    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(TicketType))]
    public string TicketTypeId { get; set; } = null!;

    [Required(ErrorMessage = "CreatedBy employee is required")]
    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(CreatedByEmployee))]
    public string CreatedByEmpId { get; set; } = null!;

    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(AssignedEmployee))]
    public string? AssignedToEmpId { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [RegularExpression("Open|InProgress|Resolved|Closed", ErrorMessage = "Status must be Open, InProgress, Resolved, or Closed")]
    [Column(TypeName = "varchar(20)")]
    public string Status { get; set; }

    [Required(ErrorMessage = "CreatedAt is required")]
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "DueAt is required")]
    [Column(TypeName = "datetime")]
    public DateTime DueAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ResolvedAt { get; set; }

    public virtual TicketType? TicketType { get; set; }
    public virtual Employee? CreatedByEmployee { get; set; }
    public virtual Employee? AssignedEmployee { get; set; }

    [JsonIgnore]
    public virtual ICollection<TicketReply>? TicketReplies { get; set; }
}
