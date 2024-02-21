using System.Diagnostics.CodeAnalysis;

namespace Optix.Movies;

/// <summary>
/// Register dependencies against the service container
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Registers services for dependency injection
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<IMovieRepository, MovieRepository>();
    }
}

