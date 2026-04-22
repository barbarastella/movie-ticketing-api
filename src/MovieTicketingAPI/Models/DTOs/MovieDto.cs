using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models.DTOs;

public record CreateMovieDto(
    [Required(ErrorMessage = "O título é obrigatório.")] string Title,
    string Description,
    [Range(1, 1000, ErrorMessage = "A duração do filme deve ser maior que 0.")] int DurationMin,
    string Genre,
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 1000.00, ErrorMessage = "O preço deve ser maior que 0.")] decimal Price
);

public record UpdateMovieDto(
    [Required(ErrorMessage = "O título é obrigatório.")] string Title,
    string Description,
    [Range(1, 1000, ErrorMessage = "A duração do filme deve ser maior que 0.")] int DurationMin,
    string Genre,
    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 1000.00, ErrorMessage = "O preço deve ser maior que 0.")] decimal Price
);