using MovieTicketingAPI.Repositories;
using MovieTicketingAPI.Services;

namespace MovieTicketingAPI.Extensions;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketService, TicketService>();

        services.AddScoped<AuthService>();

        return services;
    }
}
