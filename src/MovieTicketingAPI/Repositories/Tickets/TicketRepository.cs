using Microsoft.EntityFrameworkCore;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Persistence;

namespace MovieTicketingAPI.Repositories.Tickets;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsSeatSoldAsync(Guid movieSessionId, Guid seatId)
    {
        return await _context.Tickets.AnyAsync(t =>
            t.MovieSessionId == movieSessionId &&
            t.SeatId == seatId);
    }
}
