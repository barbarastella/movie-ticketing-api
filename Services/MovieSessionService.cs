using MovieTicketingAPI.Models;
using MovieTicketingAPI.Repositories;
using MovieTicketingAPI.Models.DTOs;
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

    public async Task<MovieSession?> CreateAsync(CreateMovieSessionDto dto)
    {
       var movieExists = await _movieRepository.GetByIdAsync(dto.MovieId);
        if (movieExists == null) return null;

        var roomExists = await _roomRepository.GetByIdAsync(dto.RoomId);
        if (roomExists == null) return null;

        var session = new MovieSession
        {
            Id = Guid.NewGuid(),
            MovieId = dto.MovieId,
            RoomId = dto.RoomId,
            StartTime = dto.StartTime.ToUniversalTime()
        };

        await _movieSessionRepository.AddAsync(session);
        return session;
    }
    public async Task<MovieSession?> UpdateAsync(Guid id, UpdateMovieSessionDto dto)
    {
        var existingSession = await _movieSessionRepository.GetByIdAsync(id);
        if (existingSession == null) return null;

        var movieExists = await _movieRepository.GetByIdAsync(dto.MovieId);
        if (movieExists == null) throw new ArgumentException("O Filme informado não existe.");

        var roomExists = await _roomRepository.GetByIdAsync(dto.RoomId);
        if (roomExists == null) throw new ArgumentException("A Sala informada não existe.");

        existingSession.MovieId = dto.MovieId;
        existingSession.RoomId = dto.RoomId;
        existingSession.StartTime = dto.StartTime.ToUniversalTime();

        await _movieSessionRepository.UpdateAsync(existingSession);
        return existingSession;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _movieSessionRepository.DeleteAsync(id);
    }
}