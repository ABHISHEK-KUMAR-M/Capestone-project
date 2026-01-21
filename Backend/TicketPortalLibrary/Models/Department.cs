using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("Departments")]
public class Department
{
    [Key]
    [Column(TypeName = "varchar(5)")]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "DepartmentId must be in format D0001")]
    public string DepartmentId { get; set; } = null!;

    [Required(ErrorMessage = "Department name is required")]
    [MaxLength(25, ErrorMessage = "Department name cannot exceed 100 characters")]
    [Column(TypeName = "varchar(25)")]
    public string DepartmentName { get; set; } = null!;

    [MaxLength(50, ErrorMessage = "Description cannot exceed 255 characters")]
    [Column(TypeName = "varchar(50)")]
    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Employee>? Employees { get; set; }

    [JsonIgnore]
    public virtual ICollection<TicketType>? TicketTypes { get; set; }
}

