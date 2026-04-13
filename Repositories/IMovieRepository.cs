using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Guid id);
    }
}
