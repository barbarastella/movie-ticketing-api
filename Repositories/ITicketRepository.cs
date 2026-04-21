using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket);
    Task<bool> IsSeatSoldAsync(Guid movieSessionId, Guid seatId);
}