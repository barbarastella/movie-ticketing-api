using MovieTicketingAPI.Repositories.Movies;
using MovieTicketingAPI.Repositories.MovieSessions;
using MovieTicketingAPI.Repositories.Rooms;
using MovieTicketingAPI.Repositories.SeatLock;
using MovieTicketingAPI.Repositories.Seats;
using MovieTicketingAPI.Repositories.Tickets;
using MovieTicketingAPI.Services.Authentication;
using MovieTicketingAPI.Services.Movies;
using MovieTicketingAPI.Services.MovieSessions;
using MovieTicketingAPI.Services.Rooms;
using MovieTicketingAPI.Services.Seats;
using MovieTicketingAPI.Services.Tickets;

namespace MovieTicketingAPI.Extensions;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketService, TicketService>();

        services.AddScoped<IMovieSessionRepository, MovieSessionRepository>();
        services.AddScoped<IMovieSessionService, MovieSessionService>();

        services.AddScoped<ISeatLockRepository, RedisSeatLockRepository>();

        services.AddScoped<IRoomRepository,  RoomRepository>();
        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<ISeatService, SeatService>();

        services.AddScoped<AuthService>();

        return services;
    }
}
