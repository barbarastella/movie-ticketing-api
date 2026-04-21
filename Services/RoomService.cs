using MovieTicketingAPI.Models;
using MovieTicketingAPI.Repositories.Rooms;

namespace MovieTicketingAPI.Services.Rooms;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _roomRepository.GetAllAsync();
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _roomRepository.GetByIdAsync(id);
    }

    public async Task<Room> CreateRoomAsync(string name)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        await _roomRepository.AddAsync(room);

        return room;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _roomRepository.DeleteAsync(id);
    }
}