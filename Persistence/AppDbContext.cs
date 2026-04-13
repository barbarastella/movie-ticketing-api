using Microsoft.EntityFrameworkCore;
using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Movie> Movies { get; set; }
    }
}
