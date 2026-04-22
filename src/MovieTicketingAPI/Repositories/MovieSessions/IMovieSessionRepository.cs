using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories.MovieSessions;
public interface IMovieSessionRepository
{
    Task<IEnumerable<MovieSession>> GetAllAsync();
    Task<MovieSession?> GetByIdWithMovieAsync(Guid id);
    Task<MovieSession?> GetByIdAsync(Guid id);
    Task AddAsync(MovieSession session);
    Task UpdateAsync(MovieSession session);
    Task DeleteAsync(Guid id);
}
