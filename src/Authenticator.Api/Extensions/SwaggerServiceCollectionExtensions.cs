using System.Diagnostics.CodeAnalysis;
using Authenticator.Api.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Authenticator.Api.Swagger.OptionsProvider;

namespace Authenticator.Api.Extensions;

/// <summary>
///     Provides extension methods for configuring swagger and swagger UI.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SwaggerServiceCollectionExtensions
{
    /// <summary>
    /// Adds the swagger dependencies to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerDependencies(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(SetApExplorerOptions);
        services.AddApiVersioning(options => { options.ReportApiVersions = true; });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(SetSwaggerGenOptions);

        return services;
    }
}