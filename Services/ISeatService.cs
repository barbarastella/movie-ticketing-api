using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Services.Seats;

public interface ISeatService
{
    Task<IEnumerable<Seat>> GetByRoomAsync(Guid roomId);
    Task<Seat?> GetByIdAsync(Guid id);
    Task<Seat?> CreateAsync(string seatNumber, Guid roomId);
    Task DeleteAsync(Guid id);
}