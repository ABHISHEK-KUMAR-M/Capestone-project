using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("TicketType")]
public class TicketType
{
    [Key]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "Invalid format. Example: B0001,J0001")]
    public string TicketTypeId { get; set; }

    [Required(ErrorMessage = "Ticket type name is required.")]
    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(Department))]
    public string DepartmentId { get; set; }

    [Required]
    [ForeignKey(nameof(SLA))]
    public string SlaId { get; set; }
    public virtual Department? Department { get; set; } = null!;
    public virtual SLA? SLA { get; set; }=null!;
    [JsonIgnore]
    public virtual ICollection<Ticket>? Tickets { get; set; } = [];
}
