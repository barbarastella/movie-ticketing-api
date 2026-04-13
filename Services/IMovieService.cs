using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(Guid id);
        Task CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(Guid id, Movie movie);
        Task DeleteMovieAsync(Guid id);
    }
}
