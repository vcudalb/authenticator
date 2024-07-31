using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Validation.GlobalExceptionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Domain.Validation.Extensions;

[ExcludeFromCodeCoverage]
public static class GlobalExceptionServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}