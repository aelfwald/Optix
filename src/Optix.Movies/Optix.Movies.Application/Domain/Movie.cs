namespace Optix.Movies;

/// <summary>
/// Defines a movie
/// </summary>
public class Movie
{
    /// <summary>
    /// The movie release date
    /// </summary>
    public DateTime ReleaseDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// The movie title
    /// </summary>
    public string Title  {   get; set;  } = "";
    
    /// <summary>
    /// The movie overview
    /// </summary>
    public string Overview { get; set; } = "";

    /// <summary>
    /// Movie popularity rating
    /// </summary>
    public float Popularity { get; set; } = 0;

    /// <summary>
    /// Number of votes the moves has received
    /// </summary>
    public int VoteCount { get; set; } = 0;

    /// <summary>
    /// Average vote value
    /// </summary>
    public float VoteAverage { get; set; } = 0;       
        
    /// <summary>
    /// The movie's original language
    /// </summary>
    public string OriginalLanguage { get; set; } = "";
    
    /// <summary>
    /// The movie genre
    /// </summary>
    public string Genre { get; set; } = "";   
    
    /// <summary>
    /// The movie poster url
    /// </summary>
    public string PosterUrl { get; set; } = "";

}
