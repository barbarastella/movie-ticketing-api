using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models;

public class Room
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public ICollection<MovieSession> MovieSessions { get; set; } = new List<MovieSession>();
}
