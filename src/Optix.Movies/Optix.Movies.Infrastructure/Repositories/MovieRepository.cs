
using Microsoft.EntityFrameworkCore;

namespace Optix.Movies;

/// <summary>
/// Repository  class for the Movie entity object
/// </summary>
/// <param name="moviesDbContext">The movie db context</param>
public class MovieRepository(MoviesDbContext moviesDbContext) : IMovieRepository
{
    private readonly MoviesDbContext _moviesDbContext = moviesDbContext;

    public async Task<IEnumerable<Movie>> SearchMovies(
        string title,
        string genre
        )
    {

        return await _moviesDbContext
            .Movies
            .Where(m =>
                (title == string.Empty || m.Title.ToUpper().Contains(title.ToUpper()))
                &&
                (genre == string.Empty || m.Genre.ToUpper().Contains(genre.ToUpper()))
                )
            .ToListAsync();
    }
}

