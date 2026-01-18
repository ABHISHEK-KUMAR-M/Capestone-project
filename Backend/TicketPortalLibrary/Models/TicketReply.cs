using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPortalLibrary.Models;

[Table("TicketReply")]
public class TicketReply
{
    [Key]
    public int ReplyId { get; set; }

    [Required]
    [ForeignKey(nameof(Ticket))]
    public int TicketId { get; set; }

    [Required]
    [ForeignKey(nameof(Employee))]
    public int RepliedByEmpId { get; set; }

    [Required(ErrorMessage = "Reply message is required.")]
    [MaxLength(2000)]
    public string Message { get; set; } = null!;

    [Required]
    public DateTime CreatedAt { get; set; } 

    public virtual Ticket Ticket { get; set; } = null!;
    public virtual Employee Employee { get; set; } = null!;
}
