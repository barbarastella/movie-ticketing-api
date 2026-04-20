using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models.DTOs;

public class BuyTicketDto
{
    [Required(ErrorMessage = "O ID do filme é obrigatório.")]
    public Guid MovieId { get; set; }
}
