using MovieTicketingAPI.Models;
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

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateMovieAsync(Movie movie)
        {;
            if (movie.DurationMin <= 0) throw new Exception("A duração do filme deve ser maior que 0 minutos.");

            await _repository.AddAsync(movie);
        }

        public async Task UpdateMovieAsync(Guid id, Movie movie)
        {
            var existingMovie = await _repository.GetByIdAsync(id);
            if (existingMovie == null) throw new Exception("Filme não encontrado");

            await _repository.UpdateAsync(movie);
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
