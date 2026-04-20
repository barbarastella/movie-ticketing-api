using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task<Movie> CreateAsync(CreateMovieDto dto);
        Task UpdateAsync(Guid id, Movie movie);
        Task DeleteAsync(Guid id);
    }
}
