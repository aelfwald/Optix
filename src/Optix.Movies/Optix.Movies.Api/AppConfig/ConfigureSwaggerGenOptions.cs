
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace Optix.Movies;

/// <summary>
/// Configuration swagger documentation generation options.
/// </summary>
/// <param name="versionProvider"></param>
public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionProvider) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _versionProvider = versionProvider;

    /// <summary>
    /// Performs the set-up of the configuration.
    /// </summary>
    public void Configure(SwaggerGenOptions options)
    {
        this.ConfigureDocuments(options);
        ConfigureXmlComments(options);
        ConfigureSchemas(options);
    }

    /// <summary>
    /// Generate a new focument for each version of the API
    /// </summary>
    /// <param name="options">The options object to apply the changes to</param>
    private void ConfigureDocuments(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription versionDescription in _versionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                versionDescription.GroupName,
                new OpenApiInfo()
                {
                    Version = versionDescription.ApiVersion.ToString(),
                    Title = "Optix.Movies",
                    Description = "API that allows access to Movie data."
                }
            );
        }
    }

    /// <summary>
    /// Use XML comments for swagger text
    /// </summary>
    /// <param name="options">The options object to apply the changes to</param>
    private static void ConfigureXmlComments(SwaggerGenOptions options)
    {
        string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }

    /// <summary>
    /// Configure how the schemas are displayed within swagger
    /// </summary>
    /// <param name="options"></param>
    private static void ConfigureSchemas(SwaggerGenOptions options)
    {
        options.CustomSchemaIds(s => s.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? s.Name);
    }
}

