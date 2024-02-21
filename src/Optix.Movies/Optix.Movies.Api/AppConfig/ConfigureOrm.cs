using Microsoft.EntityFrameworkCore;

namespace Optix.Movies;

internal static class DependConfigureOrm
{

    internal static void RegisterOrm(
            this IServiceCollection services, 
            IConfiguration configuration)
    {

        services.AddDbContext<MoviesDbContext>(options =>
        {

            string connectionString = configuration
                .GetConnectionString("MoviesDatabase")!
                .Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory);
            options.UseSqlite(connectionString);
        });
    }
}

