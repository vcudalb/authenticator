using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authenticator.Api.Swagger;

/// <summary>
/// Configures the Swagger generation options.
/// </summary>
/// <remarks>This allows API versioning to define a Swagger document per API version after the
/// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
[ExcludeFromCodeCoverage]
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private const string DeprecatedMessage = " This API version has been deprecated.";
    private const string Title = "Authenticator REST APIs.";
    private const string MainDescription = "APIs to manage authenticator resources.";
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider"> 
    ///     An <see cref="IApiVersionDescriptionProvider"/> used to generate Swagger documents.
    /// </param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this._provider = provider;

    /// <summary>
    /// Invoked to configure a TOptions instance
    /// </summary>
    /// <param name="options">
    ///      An <see cref="SwaggerGenOptions"/> the options instance to configure.
    /// </param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) =>
        GetAuthenticatorApiInfo(ref description);

    private static OpenApiInfo GetAuthenticatorApiInfo(ref ApiVersionDescription description) => new()
    {
        Title = Title,
        Version = description.ApiVersion.ToString(),
        Description = MainDescription + (description.IsDeprecated ? DeprecatedMessage : "")
    };
}