using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services.Rooms;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(Guid id);
    Task<Room> CreateAsync(CreateRoomDto dto);
    Task<Room?> UpdateAsync(Guid id, UpdateRoomDto dto);
    Task DeleteAsync(Guid id);
}
