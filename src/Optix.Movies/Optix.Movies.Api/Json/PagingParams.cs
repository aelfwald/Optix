namespace Optix.Movies.Api.Model;

/// <summary>
/// Used to pass paging parameters
/// </summary>
public class PagingParameters
{
    /// <summary>
    /// The page number to retrieve.
    /// If specified must be greater than 1.
    /// </summary>
    public int? pageNumber { get; set; }

    /// <summary>
    /// The number of rows to retrieve in the page.
    /// Only takes effect if page number is supplied.
    /// If specified must be greater than 1.
    /// If no page size is provided then a default page size
    /// of 10 will be used.
    /// </summary>
    public int? pageSize { get; set; }

}

