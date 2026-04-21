using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
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

    public async Task<Room> CreateAsync(CreateRoomDto dto)
    {
        var room = new Room
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _roomRepository.AddAsync(room);
        return room;
    }
    public async Task<Room?> UpdateAsync(Guid id, UpdateRoomDto dto)
    {
        var existingRoom = await _roomRepository.GetByIdAsync(id);
        if (existingRoom == null) return null;

        existingRoom.Name = dto.Name;

        await _roomRepository.UpdateAsync(existingRoom);
        return existingRoom;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _roomRepository.DeleteAsync(id);
    }
}