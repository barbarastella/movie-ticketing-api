using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.Movie)
            .WithMany()
            .HasForeignKey(t => t.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
