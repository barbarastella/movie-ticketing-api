using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Services.MovieSessions;

public interface IMovieSessionService
{
    Task<IEnumerable<MovieSession>> GetAllAsync();
    Task<MovieSession?> GetByIdAsync(Guid id);
    Task<MovieSession?> CreateSessionAsync(Guid movieId, Guid roomId, DateTime startTime);
    Task DeleteAsync(Guid id);
}