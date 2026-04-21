using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketingAPI.Models;

public class MovieSession
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }

    [Required]
    public Guid RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public Room? Room { get; set; }
}
