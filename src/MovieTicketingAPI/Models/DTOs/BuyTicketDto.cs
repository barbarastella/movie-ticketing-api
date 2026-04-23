using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models.DTOs;
public record BuyTicketDto(
    [Required] Guid MovieSessionId,
    [Required] Guid SeatId
);