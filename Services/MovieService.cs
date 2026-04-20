using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories;

namespace MovieTicketingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Movie> CreateAsync(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                DurationMin = dto.DurationMin,
                Genre = dto.Genre,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(movie);
            return movie;
        }

        public async Task UpdateAsync(Guid id, Movie movie)
        {
            var existingMovie = await _repository.GetByIdAsync(id);
            if (existingMovie == null) throw new Exception("Filme não encontrado");

            await _repository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
