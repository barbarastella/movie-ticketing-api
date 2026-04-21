using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories;

namespace MovieTicketingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await _movieRepository.GetByIdAsync(id);
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
                Price = dto.Price
            };

            await _movieRepository.AddAsync(movie);
            return movie;
        }

        public async Task UpdateAsync(Guid id, Movie movie)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null) throw new Exception("Filme não encontrado");

            await _movieRepository.UpdateAsync(movie);
        }
        public async Task<Movie?> UpdateAsync(Guid id, UpdateMovieDto dto)
        {
            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null) return null;

            existingMovie.Title = dto.Title;
            existingMovie.Description = dto.Description;
            existingMovie.DurationMin = dto.DurationMin;
            existingMovie.Genre = dto.Genre;
            existingMovie.Price = dto.Price;

            await _movieRepository.UpdateAsync(existingMovie);
            return existingMovie;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _movieRepository.DeleteAsync(id);
        }

    }
}
