namespace MovieTicketingAPI.Models.DTOs;

public record CreateSeatDto(string SeatNumber, Guid RoomId);
public record UpdateSeatDto(string SeatNumber, Guid RoomId);