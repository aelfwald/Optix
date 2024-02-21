using Microsoft.EntityFrameworkCore;

namespace Optix.Movies;

public class MoviesDbContext : DbContext
{
    public virtual DbSet<Movie> Movies { get; set; }

    public MoviesDbContext()
    {
    }

    public MoviesDbContext(DbContextOptions options) : base(options)
    {  
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasNoKey();
    }
}
