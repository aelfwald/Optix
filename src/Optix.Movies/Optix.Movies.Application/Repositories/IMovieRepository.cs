namespace Optix.Movies
{
    /// <summary>
    /// Defines a movie repository
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Movies"/></returns>
        Task<IEnumerable<Movie>> SearchMovies(
            string title,
            string genre
            );
    }
}
