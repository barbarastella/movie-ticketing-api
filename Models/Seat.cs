using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketingAPI.Models;

public class Seat
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string SeatNumber { get; set; } = string.Empty;

    [Required]
    public Guid RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public Room? Room { get; set; }
}
