namespace Optix.Movies;

/// <summary>
/// Defines a service for movies
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Searches the movies
    /// </summary>
    /// <param name="title">The movie title</param>
    /// <param name="genre">The movie genre</param>
    /// <param name="page">The page number requested</param>
    /// <param name="pageSize">The page size requested</param>
    /// <param name="limit">The number of results to return</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Movies"/></returns>
    Task<IEnumerable<Movie>> GetMovies(
        string title = "",
        string genre = "",
        int? page = null,
        int? pageSize = 20,
        int? limit = null);
}

