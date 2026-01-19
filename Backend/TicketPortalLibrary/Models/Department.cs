using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("Departments")]
public class Department
{
    [Key]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "Invalid format. Example: B0001,J0001")]
    public string DepartmentId { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = null!;

    [MaxLength(255)]
    public string? Description { get; set; }
    [JsonIgnore]
    public virtual ICollection<Employee>? Employees { get; set; } = [];
    [JsonIgnore]
    public virtual ICollection<TicketType>? TicketTypes { get; set; } = [];
}
