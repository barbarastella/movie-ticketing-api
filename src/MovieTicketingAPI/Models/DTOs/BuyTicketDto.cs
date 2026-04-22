using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models.DTOs;

public class BuyTicketDto
{
    [Required]
    public Guid MovieSessionId { get; set; }

    [Required]
    public Guid SeatId { get; set; }
}
