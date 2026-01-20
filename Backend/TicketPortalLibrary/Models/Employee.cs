using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TicketPortalLibrary.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    [Column(TypeName = "varchar(5)")]
    [RegularExpression(@"^[A-Z]\d{4}$",ErrorMessage = "EmployeeId must be in format E0001")]
    public string EmpId { get; set; } = null!;

    [Required(ErrorMessage = "Employee name is required")]
    [MaxLength(30, ErrorMessage = "Employee name cannot exceed 30 characters")]
    [Column(TypeName = "varchar(30)")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Password cannot exceed 50 characters")]
    [Column(TypeName = "varchar(50)")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("Admin|User", ErrorMessage = "Role must be either Admin or User")]
    [Column(TypeName = "varchar(20)")]
    public string Role { get; set; } = null!;

    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(Department))]
    public string? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket>? CreatedTickets { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ticket>? AssignedTickets { get; set; }

    [JsonIgnore]
    public virtual ICollection<TicketReply>? ReplyToCreatedTickets { get; set; }

    [JsonIgnore]
    public virtual ICollection<TicketReply>? ReplyToAssignedTickets { get; set; }

}
