using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Persistence.Contexts;
using Duende.IdentityServer.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authenticator.Api.Extensions;

/// <summary>
///     Provides extension methods for configuring identity server.
/// </summary>
[ExcludeFromCodeCoverage]
public static class IdentityServerServiceCollectionExtensions
{
    /// <summary>
    ///     Settings up identity dependencies. 
    /// </summary>
    /// <param name="services">
    ///     A <see cref="IServiceCollection"/> specifies the contract for a collection of service descriptors.
    /// </param>
    /// <param name="configuration">
    ///     A <see cref="IConfiguration"/> represents a set of key/value application configuration properties.
    /// </param>
    /// <returns></returns>
    public static IServiceCollection AddDuendeIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        const string infrastructureAssembly = "Authenticator.Infrastructure";
        string connectionString = configuration.GetConnectionString("authenticator");
        services.AddDbContext<AuthenticatorDbContext>(options =>
            options.UseSqlServer(connectionString,  m => m.MigrationsAssembly(infrastructureAssembly).EnableRetryOnFailure()));
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.ClaimsIdentity.UserIdClaimType = "id";
            })
            .AddEntityFrameworkStores<AuthenticatorDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<SecurityStampValidatorOptions>(o => o.ValidationInterval = TimeSpan.FromMinutes(5));

        services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSlidingExpiration = true;
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(5);
                options.IssuerUri = configuration.GetValue<string>("AuthSettings:AuthIssuer");
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<User>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, m => m.MigrationsAssembly(infrastructureAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, m => m.MigrationsAssembly(infrastructureAssembly));
            });
        //.AddProfileService<ProfileService>()
        //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

        return services;
    }
}