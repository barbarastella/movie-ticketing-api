namespace MovieTicketingAPI.Models.DTOs;

public record CreateMovieSessionDto(Guid MovieId, Guid RoomId, DateTime StartTime);
public record UpdateMovieSessionDto(Guid MovieId, Guid RoomId, DateTime StartTime);