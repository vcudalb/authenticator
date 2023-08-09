using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authenticator.Api.Swagger;

/// <summary>
/// A document filter replacing v{version:apiVersion} with the real version of the corresponding swagger doc
/// </summary>
[ExcludeFromCodeCoverage]
public class ReplaceVersionDocumentFilter : IDocumentFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        OpenApiPaths openApiPaths = new();

        foreach (var path in swaggerDoc.Paths)
        {
            openApiPaths.Add(path.Key.Replace("v{version}", $"{swaggerDoc.Info.Version}"), path.Value);
        }

        swaggerDoc.Paths = openApiPaths;
    }
}