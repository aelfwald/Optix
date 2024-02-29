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

        PagingRequirements? pagingRequirements = null;

        // Pagination
        if (page.HasValue)
        {
            //Default page size to 10 if no page size provided
            if (!pageSize.HasValue)
            {
                pageSize = 10;
            }

            pagingRequirements = new PagingRequirements()
            {
                Page = page.Value,
                PageSize = pageSize.Value
            };    
        }

        IEnumerable<Movie> movies = await _movieRespository.SearchMovies(
            title, genre, limit, pagingRequirements);

        return movies.Take(100);
    }
}

