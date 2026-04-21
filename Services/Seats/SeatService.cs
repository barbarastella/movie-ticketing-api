using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories.Rooms;
using MovieTicketingAPI.Repositories.Seats;

namespace MovieTicketingAPI.Services.Seats;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IRoomRepository _roomRepository;

    public SeatService(ISeatRepository seatRepository, IRoomRepository roomRepository)
    {
        _seatRepository = seatRepository;
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Seat>> GetByRoomAsync(Guid roomId)
    {
        return await _seatRepository.GetByRoomAsync(roomId);
    }

    public async Task<Seat?> GetByIdAsync(Guid id)
    {
        return await _seatRepository.GetByIdAsync(id);
    }

    public async Task<Seat?> CreateAsync(string seatNumber, Guid roomId)
    {
        var roomExists = await _roomRepository.GetByIdAsync(roomId);
        if (roomExists == null) return null;

        var seat = new Seat
        {
            Id = Guid.NewGuid(),
            SeatNumber = seatNumber,
            RoomId = roomId
        };

        await _seatRepository.AddAsync(seat);
        return seat;
    }

    public async Task<Seat?> UpdateAsync(Guid id, UpdateSeatDto dto)
    {
        var existingSeat = await _seatRepository.GetByIdAsync(id);
        if (existingSeat == null) return null;

        var roomExists = await _roomRepository.GetByIdAsync(dto.RoomId);
        if (roomExists == null) return null;

        existingSeat.SeatNumber = dto.SeatNumber;
        existingSeat.RoomId = dto.RoomId;

        await _seatRepository.UpdateAsync(existingSeat);
        return existingSeat;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _seatRepository.DeleteAsync(id);
    }
}