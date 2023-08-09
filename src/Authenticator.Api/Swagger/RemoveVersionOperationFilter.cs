using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authenticator.Api.Swagger;

/// <summary>
/// An operation filter removing version from parameters
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveVersionOperationFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParameter = operation.Parameters.SingleOrDefault(p => string.Equals(p.Name, "version", StringComparison.InvariantCulture));
        if (versionParameter is not null) operation.Parameters.Remove(versionParameter);
    }
}