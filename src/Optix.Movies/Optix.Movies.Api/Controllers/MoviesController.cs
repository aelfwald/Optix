using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Optix.Movies.Api.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Optix.Movies;

/// <summary>
/// Movies REST controller.
/// </summary>
/// <param name="movieService">The movie service class</param>
/// <param name="logger">The logger</param>
[ApiController]
[Route("api/movies")]
public class MoviesController(
    IMovieService movieService,
    ILogger<MoviesController> logger) : ControllerBase
{
    private readonly IMovieService _movieService = movieService;
    private readonly ILogger<MoviesController> _logger = logger;

    /// <summary>
    /// Provides movie search functionality
    /// </summary>
    /// <param name="title">The movie title to search for. Performs a case-insensitive LIKE search.</param>
    /// <param name="genre">Freetext search by movie genre. Performs a case-insensitive LIKE search.</param>
    /// <param name="limit">The maximum number of results to return.</param>
    /// <param name="pagingParameters">Defines the user's paging requirements.</param>
    [HttpGet()]
    [SwaggerResponse(200, Type = typeof(List<MovieJson>))]
    [SwaggerResponse(400, "Bad request")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<ActionResult<List<MovieJson>>> GetMovies(
        [FromQuery] string title = "",
        [FromQuery] string genre = "",
        [FromQuery] int? limit = null,
        [FromQuery] PagingParameters? pagingParameters = null
        )
    {

        if (!ValidateParams(limit, pagingParameters, out ModelStateDictionary modelStateDictionary))
        {
            return BadRequest(modelStateDictionary);
        }

        List<MovieJson> movies = [];

        foreach (var item in await _movieService.GetMovies(
            title,
            genre,
            pagingParameters?.pageNumber,
            pagingParameters?.pageSize,
            limit))
        {
            movies.Add(item.ToVm());
        }

        return Ok(movies);
    }

    private static bool ValidateParams(
        int? limit,
        PagingParameters? pagingParameters,
        out ModelStateDictionary modelStateDictionary)
    {

        modelStateDictionary = new ModelStateDictionary();

        if (limit.HasValue && limit < 0)
        {
            modelStateDictionary.AddModelError("limit", "If provided, limit must be greater than or equal to zero.");
        }

        if ((pagingParameters!.pageNumber ?? 1) < 1)
        {
            modelStateDictionary.AddModelError("pageNumber", "If provided, limit must be greater than or equal to one.");

            if ((pagingParameters!.pageSize ?? 1) < 1)
            {
                modelStateDictionary.AddModelError("pageSize", "If provided, limit must be greater than or equal to one.");
            }
        }

        return !modelStateDictionary.Keys.Any();
    }
}

