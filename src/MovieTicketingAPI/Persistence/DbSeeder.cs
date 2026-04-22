using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.Enums;

namespace MovieTicketingAPI.Persistence;

public static class DbSeeder
{
    public static void SeedData(AppDbContext context)
    {
        // superuser
        if (!context.Users.Any(u => u.Role == UserRole.Admin))
        {
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrador",
                Email = "admin@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = UserRole.Admin
            };

            context.Users.Add(adminUser);
        }

        if (!context.Movies.Any() && !context.Rooms.Any())
        {
            // movies
            var movie1 = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Interestelar",
                Description = "Uma equipe de exploradores viaja através de um buraco de minhoca no espaço.",
                DurationMin = 169,
                Genre = "Ficção Científica",
                Price = 14.75m
            };

            var movie2 = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "O Auto da Compadecida",
                Description = "As aventuras de João Grilo e Chicó.",
                DurationMin = 104,
                Genre = "Comédia",
                Price = 14.75m
            };

            context.Movies.AddRange(movie1, movie2);

            // rooms
            var room1 = new Room { Id = Guid.NewGuid(), Name = "Sala 1 - IMAX" };
            var room2 = new Room { Id = Guid.NewGuid(), Name = "Sala 2 - VIP" };

            context.Rooms.AddRange(room1, room2);

            // seats
            var seats = new List<Seat>();

            for (int i = 1; i <= 5; i++)
            {
                seats.Add(new Seat { Id = Guid.NewGuid(), SeatNumber = $"A{i}", RoomId = room1.Id });
                seats.Add(new Seat { Id = Guid.NewGuid(), SeatNumber = $"B{i}", RoomId = room2.Id });
            }

            context.Seats.AddRange(seats);

            // moviesessions
            var session1 = new MovieSession
            {
                Id = Guid.NewGuid(),
                MovieId = movie1.Id,
                RoomId = room1.Id,
                StartTime = DateTime.UtcNow.AddDays(1).AddHours(19)
            };

            var session2 = new MovieSession
            {
                Id = Guid.NewGuid(),
                MovieId = movie2.Id,
                RoomId = room2.Id,
                StartTime = DateTime.UtcNow.AddDays(2).AddHours(21)
            };

            context.MovieSessions.AddRange(session1, session2);
        }

        context.SaveChanges();
    }
}