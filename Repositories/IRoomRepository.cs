using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories.Rooms;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(Guid id);
    Task AddAsync(Room room);
    Task DeleteAsync(Guid id);
}