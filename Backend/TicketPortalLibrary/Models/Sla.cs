using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("SLAs")]
public class SLA
{
    [Key]
    public int SlaId { get; set; }

    [Required(ErrorMessage = "Response time is required.")]
    [Range(1, 168)]
    public int ResponseTime { get; set; }   // hours

    [Required(ErrorMessage = "Resolution time is required.")]
    [Range(1, 720)]
    public int ResolutionHours { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }
    public virtual ICollection<TicketType> TicketTypes { get; set; } = [];
}
