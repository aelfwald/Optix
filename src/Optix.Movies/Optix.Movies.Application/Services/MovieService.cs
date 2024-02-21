namespace Optix.Movies;

/// <summary>
/// A service that provides movie functionality.
/// </summary>
/// <param name="movieRepository">The movie repository.</param>
public class MovieService(IMovieRepository movieRepository) : IMovieService
{
    private readonly IMovieRepository _movieRespository = movieRepository;

    public async Task<IEnumerable<Movie>> GetMovies(
        string title = "",
        string genre = "",
        int? page = null,
        int? pageSize = null,
        int? limit = null)
    {
        IEnumerable<Movie> movies = await _movieRespository.SearchMovies(title, genre);

        // Apply any search limit requested
        if (limit.HasValue)
        {
            movies = movies.Take(limit.Value);
        }

        // Pagination
        if (page.HasValue)
        {
            int take = pageSize ?? 10;
            int skip = (page.Value - 1) * take;
            movies = movies.Skip(skip).Take(take);
        }

        return movies;
    }
}

