using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(Guid id);
    Task<Room> CreateRoomAsync(string name);
    Task DeleteAsync(Guid id);
}
