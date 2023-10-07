using System.Diagnostics.CodeAnalysis;
using Authenticator.Application.IdentityServer.ServiceProviders;
using Authenticator.Application.IdentityServer.ServiceProviders.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Application.IdentityServer.Extensions;

[ExcludeFromCodeCoverage]
public static class IdentityServerServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}