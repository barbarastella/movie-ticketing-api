using Microsoft.EntityFrameworkCore;
using MovieTicketingAPI.Persistence;
using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await GetByIdAsync(id);

            if (movie != null) await _context.Movies.Where(m => m.Id == id).ExecuteDeleteAsync();
        }
    }
}
