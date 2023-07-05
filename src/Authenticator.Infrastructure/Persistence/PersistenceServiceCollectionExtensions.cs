using System.Diagnostics.CodeAnalysis;
using Authenticator.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Infrastructure.Persistence;

/// <summary>
///     Provides extension methods used to configure sql server service providers.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthenticatorDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("authenticator"),
                builder => builder.MigrationsAssembly(typeof(AuthenticatorDbContext).Assembly.FullName)));

        return services;
    }
}