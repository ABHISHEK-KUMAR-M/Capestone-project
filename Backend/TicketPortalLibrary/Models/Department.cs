using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("Departments")]
public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = null!;

    [MaxLength(255)]
    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = [];
    public virtual ICollection<TicketType> TicketTypes { get; set; } = [];
}
