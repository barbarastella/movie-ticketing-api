using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services.Seats;

public interface ISeatService
{
    Task<IEnumerable<Seat>> GetByRoomAsync(Guid roomId);
    Task<Seat?> GetByIdAsync(Guid id);
    Task<Seat?> CreateAsync(string seatNumber, Guid roomId);
    Task<Seat?> UpdateAsync(Guid id, UpdateSeatDto dto);
    Task DeleteAsync(Guid id);
}