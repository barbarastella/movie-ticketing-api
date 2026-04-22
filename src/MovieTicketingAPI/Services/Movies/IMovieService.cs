using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services.Movies
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task<Movie> CreateAsync(CreateMovieDto dto);
        Task<Movie?> UpdateAsync(Guid id, UpdateMovieDto dto);
        Task DeleteAsync(Guid id);
    }
}
