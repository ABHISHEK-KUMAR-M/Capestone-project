using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("TicketType")]
public class TicketType
{
    [Key]
    public int TicketTypeId { get; set; }

    [Required(ErrorMessage = "Ticket type name is required.")]
    [MaxLength(100)]
    public string TypeName { get; set; } = null!;

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    [Required]
    [ForeignKey(nameof(SLA))]
    public int SlaId { get; set; }
    public virtual Department Department { get; set; } = null!;
    public virtual SLA SLA { get; set; }=null!;
    public virtual ICollection<Ticket> Tickets { get; set; } = [];
}
