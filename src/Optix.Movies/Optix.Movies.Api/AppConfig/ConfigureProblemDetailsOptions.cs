namespace Optix.Movies;

/// <summary>
/// Configures problem details
/// </summary>
public class ConfigureProblemDetailsOptions
{
    /// <summary>
    /// Configure the problem details middleware to transform any exceptions
    /// into a properly formatted problem details response
    /// </summary>
    public static void ConfigureProblemDetails(ProblemDetailsOptions options)
    {
        options.CustomizeProblemDetails = Configure;
    }

    private static void Configure(ProblemDetailsContext context)
    {
        HandleDefaultException(context);
    }

    private static void HandleDefaultException(ProblemDetailsContext context)
    {
        context.ProblemDetails.Type = "https://httpstatuses.io/500";
        context.ProblemDetails.Title = "Internal Server Error";
        context.ProblemDetails.Status = 500;
        context.ProblemDetails.Detail = "An internal server has occured while processing the request";
    }
}

