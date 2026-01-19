using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketPortalLibrary.Models;

[Table("SLAs")]
public class SLA
{
    [Key]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "Invalid format. Example: B0001,J0001")]
    public string SlaId { get; set; }

    [Required(ErrorMessage = "Response time is required.")]
    [Range(1, 168)]
    public int ResponseTime { get; set; }   // hours

    [Required(ErrorMessage = "Resolution time is required.")]
    [Range(1, 720)]
    public int ResolutionHours { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }
    [JsonIgnore]
    public virtual ICollection<TicketType>? TicketTypes { get; set; } = [];
}
