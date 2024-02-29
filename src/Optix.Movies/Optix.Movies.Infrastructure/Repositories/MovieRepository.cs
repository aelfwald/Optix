
using Microsoft.EntityFrameworkCore;

namespace Optix.Movies;

/// <summary>
/// Repository class for the Movie entity object
/// </summary>
/// <param name="moviesDbContext">The movie db context</param>
public class MovieRepository(MoviesDbContext moviesDbContext) : IMovieRepository
{
    private readonly MoviesDbContext _moviesDbContext = moviesDbContext;

    public async Task<IEnumerable<Movie>> SearchMovies(
        string title = "",
        string genre = "",
        int? searchLimit = null,
        PagingRequirements? pagingRequirements = null
    )
    {
        IQueryable<Movie> query = _moviesDbContext.Movies;

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(m => m.Title.ToLower().Contains(title.ToLower()));
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => m.Genre.ToLower().Contains(genre.ToLower()));
        }

        if (searchLimit.HasValue)
        {
            query = query.Take(searchLimit.Value);
        }

        if (pagingRequirements != null)
        {
            int take = pagingRequirements.Value.PageSize;
            int skip = (pagingRequirements.Value.Page - 1) * take;

            query = query.Skip(skip).Take(take);
        }

        return await query.ToListAsync();
    }
}

