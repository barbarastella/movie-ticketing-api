using Microsoft.EntityFrameworkCore;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Persistence;

namespace MovieTicketingAPI.Repositories.Seats;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetByRoomAsync(Guid roomId)
    {
        return await _context.Seats
             .Where(s => s.RoomId == roomId)
             .AsNoTracking()
             .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(Guid id)
    {
        return await _context.Seats.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var seat = await _context.Seats.FindAsync(id);

        if (seat != null)
        {
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}