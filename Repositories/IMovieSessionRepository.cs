using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories;
public interface IMovieSessionRepository
{
    Task<IEnumerable<MovieSession>> GetAllAsync();
    Task<MovieSession?> GetByIdWithMovieAsync(Guid id);
    Task AddAsync(MovieSession session);
    Task DeleteAsync(Guid id);
}
