using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("TicketType")]
public class TicketType
{
    [Key]
    [Column(TypeName = "varchar(5)")]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "TicketTypeId must be in format T0001")]
    public string TicketTypeId { get; set; } = null!;

    [Required(ErrorMessage = "Ticket type name is required")]
    [MaxLength(100, ErrorMessage = "Ticket type name cannot exceed 100 characters")]
    [Column(TypeName = "varchar(100)")]
    public string TypeName { get; set; } = null!;

    [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    [Column(TypeName = "varchar(255)")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "DepartmentId is required")]
    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(Department))]
    public string DepartmentId { get; set; } = null!;

    [Required(ErrorMessage = "SLA Id is required")]
    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(SLA))]
    public string SlaId { get; set; } = null!;

    public virtual Department? Department { get; set; }
    public virtual SLA? SLA { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket>? Tickets { get; set; }
}
