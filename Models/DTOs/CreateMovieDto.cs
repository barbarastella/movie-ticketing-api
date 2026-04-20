using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models.DTOs;

public class CreateMovieDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(1, 1000, ErrorMessage = "A duração do filme deve ser maior que 0.")]
    public int DurationMin { get; set; }

    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "O precço é obrigatório.")]
    [Range(0.01, 1000.00, ErrorMessage = "O preço deve ser maior que 0.")]
    public decimal Price { get; set; }
}
