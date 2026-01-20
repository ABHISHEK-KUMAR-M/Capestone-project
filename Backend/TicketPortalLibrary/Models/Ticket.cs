using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    public int TicketId { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(150)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    [MaxLength(2000)]
    public string Description { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(TicketType))]
    public string TicketTypeId { get; set; }

    [Required]
    // [ForeignKey(nameof(CreatedByEmployee))]
    public string CreatedByEmpId { get; set; }

    // [ForeignKey(nameof(AssignedEmployee))]
    public string? AssignedToEmpId { get; set; }

    [Required]
    [RegularExpression("Open|InProgress|Resolved|Closed")]
    [Column(TypeName = "varchar(20)")]
    public string Status { get; set; } = "Open";

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime DueAt { get; set; }

    public DateTime? ResolvedAt { get; set; }
    public virtual TicketType? TicketType { get; set; } = null!;
    public virtual Employee? CreatedByEmployee { get; set; } = null!;
    public virtual Employee? AssignedEmployee { get; set; }
    [JsonIgnore]
    public virtual ICollection<TicketReply>? TicketReplies { get; set; } = [];
}
