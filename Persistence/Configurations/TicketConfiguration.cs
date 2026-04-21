using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTicketingAPI.Models;

namespace MovieTicketingAPI.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder
            .HasOne(t => t.MovieSession)
            .WithMany()
            .HasForeignKey(t => t.MovieSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.Seat)
            .WithMany()
            .HasForeignKey(t => t.SeatId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
