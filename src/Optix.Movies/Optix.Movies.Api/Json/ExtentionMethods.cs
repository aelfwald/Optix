namespace Optix.Movies;

internal static class ExtensionsMethods
{
    internal static MovieJson ToVm(this Movie movie)
    {
        return new MovieJson()
        {
            Genre = movie.Genre,
            OriginalLanguage = movie.OriginalLanguage,
            Overview = movie.Overview,
            Popularity = movie.Popularity,
            PosterUrl = movie.PosterUrl,
            ReleaseDate = movie.ReleaseDate,
            Title = movie.Title,
            VoteAverage = movie.VoteAverage,
            VoteCount = movie.VoteCount
        };
    }
}
