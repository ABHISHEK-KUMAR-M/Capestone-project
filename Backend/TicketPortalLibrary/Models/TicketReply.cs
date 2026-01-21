using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("TicketReply")]
public class TicketReply
{
    [Key]
    public int ReplyId { get; set; }
    [Required(ErrorMessage = "TicketId is required")]
    [ForeignKey(nameof(Ticket))]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Reply author (creator) is required")]
    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(ReplyByCreatedEmp))]
    public string RepliedByCreatorEmpId { get; set; } = null!;

    [Column(TypeName = "varchar(5)")]
    [ForeignKey(nameof(ReplyByAssignedEmp))]
    public string? RepliedByAssignedEmpId { get; set; }

    [Required(ErrorMessage = "Reply message is required")]
    [MaxLength(2000, ErrorMessage = "Reply message cannot exceed 2000 characters")]
    [Column(TypeName = "varchar(2000)")]
    public string Message { get; set; } = null!;

    [Required(ErrorMessage = "CreatedAt is required")]
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
    public virtual Ticket? Ticket { get; set; }
    public virtual Employee? ReplyByCreatedEmp { get; set; }
    public virtual Employee? ReplyByAssignedEmp { get; set; }

}