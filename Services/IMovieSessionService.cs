using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services.MovieSessions;

public interface IMovieSessionService
{
    Task<IEnumerable<MovieSession>> GetAllAsync();
    Task<MovieSession?> GetByIdAsync(Guid id);
    Task<MovieSession?> CreateAsync(CreateMovieSessionDto dto);
    Task<MovieSession?> UpdateAsync(Guid id, UpdateMovieSessionDto dto);
    Task DeleteAsync(Guid id);
}