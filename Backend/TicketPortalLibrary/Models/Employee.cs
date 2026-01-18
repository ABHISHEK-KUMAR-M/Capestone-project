using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    public int EmpId { get; set; }

    [Required(ErrorMessage = "Employee name is required.")]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8)]
    [MaxLength(256)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Role is required.")]
    [RegularExpression("Admin|User", ErrorMessage = "Role must be Admin or User.")]
    [Column(TypeName = "varchar(20)")]
    public string Role { get; set; } = null!;

    [ForeignKey(nameof(Department))]
    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
    public virtual ICollection<Ticket> CreatedTickets {get; set;}=[];
    public virtual ICollection<Ticket> AssignedTickets {get; set;}=[];
}
