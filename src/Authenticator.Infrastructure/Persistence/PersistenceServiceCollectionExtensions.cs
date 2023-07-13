using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Common;
using Authenticator.Domain.Repositories.Abstractions;
using Authenticator.Infrastructure.Persistence.Contexts;
using Authenticator.Infrastructure.Persistence.Repositories;
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

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}