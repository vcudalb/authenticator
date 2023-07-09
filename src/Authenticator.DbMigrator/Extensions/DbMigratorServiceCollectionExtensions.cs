using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.DbMigrator.Extensions;

/// <summary>
///     Provides extension methods for configuring migration layer dependencies.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DbMigratorServiceCollectionExtensions
{
    public static IServiceCollection AddIdentitiesAndIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        const string infrastructureAssembly = "Authenticator.Infrastructure";
        string connectionString = configuration.GetConnectionString("authenticator");

        services.AddDbContext<AuthenticatorDbContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(infrastructureAssembly)));
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
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
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<User>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, m => m.MigrationsAssembly(infrastructureAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, m => m.MigrationsAssembly(infrastructureAssembly));
            });

        return services;
    }
}