using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories.Seats;

public interface ISeatRepository
{
    Task<IEnumerable<Seat>> GetByRoomAsync(Guid roomId);
    Task<Seat?> GetByIdAsync(Guid id);
    Task AddAsync(Seat seat);
    Task UpdateAsync(Seat seat);
    Task DeleteAsync(Guid id);
}