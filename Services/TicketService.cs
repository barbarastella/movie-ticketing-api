using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories;

namespace MovieTicketingAPI.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMovieSessionRepository _movieSessionRepository;
    private readonly ISeatLockRepository _seatLockRepository;

    public TicketService(ITicketRepository ticketRepository, IMovieSessionRepository movieSessionRepository, ISeatLockRepository seatLockRepository)
    {
        _ticketRepository = ticketRepository;
        _movieSessionRepository = movieSessionRepository;
        _seatLockRepository = seatLockRepository;
    }

    public async Task<bool> ReserveSeatAsync(Guid userId, Guid movieSessionId, Guid seatId)
    {
        var isSold = await _ticketRepository.IsSeatSoldAsync(movieSessionId, seatId);
        if (isSold) return false; 

        var expirationTime = TimeSpan.FromMinutes(5);
        return await _seatLockRepository.LockSeatAsync(movieSessionId, seatId, userId, expirationTime);
    }

    public async Task<Ticket?> ConfirmPurchaseAsync(Guid userId, BuyTicketDto dto)
    {
        var isSold = await _ticketRepository.IsSeatSoldAsync(dto.MovieSessionId, dto.SeatId);
        if (isSold) throw new InvalidOperationException("O assento já foi vendido.");

        var isLocked = await _seatLockRepository.IsSeatLockedAsync(dto.MovieSessionId, dto.SeatId);
        if (!isLocked) throw new InvalidOperationException("O tempo de reserva esgotou.");

        var session = await _movieSessionRepository.GetByIdWithMovieAsync(dto.MovieSessionId);
        if (session == null || session.Movie == null) return null;

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MovieSessionId = dto.MovieSessionId,
            SeatId = dto.SeatId,
            PurchaseDate = DateTime.UtcNow,
            Price = session.Movie.Price
        };

        await _ticketRepository.AddAsync(ticket);
        await _seatLockRepository.ReleaseSeatLockAsync(dto.MovieSessionId, dto.SeatId);

        return ticket;
    }
}
