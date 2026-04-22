using Microsoft.EntityFrameworkCore;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Persistence;

namespace MovieTicketingAPI.Repositories.MovieSessions;

public class MovieSessionRepository : IMovieSessionRepository
{
    private readonly AppDbContext _context;

    public MovieSessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieSession>> GetAllAsync()
    {
        return await _context.MovieSessions
            .Include(ms => ms.Movie)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MovieSession?> GetByIdWithMovieAsync(Guid id)
    {
        return await _context.MovieSessions
        .Include(ms => ms.Movie)
        .AsNoTracking()
        .FirstOrDefaultAsync(ms => ms.Id == id);
    }

    public async Task<MovieSession?> GetByIdAsync(Guid id)
    {
       return await _context.MovieSessions.FirstOrDefaultAsync(ms => ms.Id == id);
    }

    public async Task AddAsync(MovieSession session)
    {
        await _context.MovieSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(MovieSession session)
    {
        _context.MovieSessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var session = await _context.MovieSessions.FindAsync(id);

        if (session != null)
        {
            _context.MovieSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}