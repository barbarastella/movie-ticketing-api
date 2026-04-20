using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories;

namespace MovieTicketingAPI.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMovieRepository _movieRepository;

    public TicketService(ITicketRepository ticketRepository, IMovieRepository movieRepository)
    {
        _ticketRepository = ticketRepository;
        _movieRepository = movieRepository;
    }

    public async Task<Ticket?> BuyAsync(Guid userId, BuyTicketDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(dto.MovieId);

        if (movie == null) return null;

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MovieId = dto.MovieId,
            PurchaseDate = DateTime.UtcNow,
            Price = movie.Price
        };

        await _ticketRepository.AddAsync(ticket);
        return ticket;
    }
}
