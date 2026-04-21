using MovieTicketingAPI.Models;
using MovieTicketingAPI.Repositories;
using MovieTicketingAPI.Repositories.Rooms;

namespace MovieTicketingAPI.Services.MovieSessions;

public class MovieSessionService : IMovieSessionService
{
    private readonly IMovieSessionRepository _movieSessionRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IRoomRepository _roomRepository;

    public MovieSessionService(
        IMovieSessionRepository movieSessionRepository,
        IMovieRepository movieRepository,
        IRoomRepository roomRepository)
    {
        _movieSessionRepository = movieSessionRepository;
        _movieRepository = movieRepository;
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<MovieSession>> GetAllAsync()
    {
        return await _movieSessionRepository.GetAllAsync();
    }

    public async Task<MovieSession?> GetByIdAsync(Guid id)
    {
         return await _movieSessionRepository.GetByIdWithMovieAsync(id);
    }

    public async Task<MovieSession?> CreateSessionAsync(Guid movieId, Guid roomId, DateTime startTime)
    {
       var movieExists = await _movieRepository.GetByIdAsync(movieId);
        if (movieExists == null) return null;

        var roomExists = await _roomRepository.GetByIdAsync(roomId);
        if (roomExists == null) return null;

        var session = new MovieSession
        {
            Id = Guid.NewGuid(),
            MovieId = movieId,
            RoomId = roomId,

            // Novamente, forçamos o padrão UTC para evitar crashes no PostgreSQL
            StartTime = startTime.ToUniversalTime()
        };

        await _movieSessionRepository.AddAsync(session);
        return session;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _movieSessionRepository.DeleteAsync(id);
    }
}