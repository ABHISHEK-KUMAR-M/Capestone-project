using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketPortalLibrary.Models;

[Table("SLAs")]
public class SLA
{
    [Key]
    [Column(TypeName = "varchar(5)")]
    [RegularExpression(@"^[A-Z]\d{4}$", ErrorMessage = "SLA Id must be in format S0001")]
    public string SlaId { get; set; } = null!;

    [Required(ErrorMessage = "Response time is required")]
    [Range(1, 168, ErrorMessage = "Response time must be between 1 and 168 hours")]
    [Column(TypeName = "int")]
    public int ResponseTime { get; set; }

    [Required(ErrorMessage = "Resolution hours are required")]
    [Range(1, 720, ErrorMessage = "Resolution hours must be between 1 and 720")]
    [Column(TypeName = "int")]
    public int ResolutionHours { get; set; }

    [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
    [Column(TypeName = "varchar(255)")]
    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<TicketType>? TicketTypes { get; set; }
}
