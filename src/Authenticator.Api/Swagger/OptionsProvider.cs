using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authenticator.Api.Swagger;

/// <summary>
/// Swagger Options Provider, represents an option service provider.
/// </summary>
public static class OptionsProvider
{
    /// <summary>
    /// Creates the <see cref="OpenApiSecurityRequirement"/> settings for the SwaggerGen registration
    /// </summary>
    public static OpenApiSecurityRequirement GetOpenApiSecurityRequirement() => new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };

    /// <summary>
    /// Creates the <see cref="OpenApiSecurityScheme"/> settings for the SwaggerGen registration
    /// </summary>
    public static OpenApiSecurityScheme GetOpenApiSecurityScheme() => new()
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    };

    /// <summary>
    /// Compose the <see cref="ApiExplorerOptions"/> settings for the SwaggerGen registration
    /// </summary>
    /// <param name="options"></param>
    public static void SetApExplorerOptions(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    }
    
    /// <summary>
    /// Compose the <see cref="SwaggerGenOptions"/> settings for the SwaggerGen registration
    /// </summary>
    /// <param name="options"></param>
    public static void SetSwaggerGenOptions(SwaggerGenOptions options)
    {
        var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
        options.IncludeXmlComments(apiXmlPath);

        options.AddSecurityDefinition("Bearer", OptionsProvider.GetOpenApiSecurityScheme());

        options.AddSecurityRequirement(OptionsProvider.GetOpenApiSecurityRequirement());

        options.OperationFilter<RemoveVersionOperationFilter>();
        options.DocumentFilter<ReplaceVersionDocumentFilter>();
        options.SchemaFilter<EnumSchemaFilter>();
    }
}