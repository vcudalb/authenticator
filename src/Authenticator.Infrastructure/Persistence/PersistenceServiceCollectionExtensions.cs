using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Common;
using Authenticator.Domain.Repositories.Abstractions;
using Authenticator.Infrastructure.Persistence.Contexts;
using Authenticator.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authenticator.Infrastructure.Persistence;

/// <summary>
///     Provides extension methods used to configure sql server service providers.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PersistenceServiceCollectionExtensions
{
    private static readonly InMemoryDatabaseRoot _inMemoryDatabaseRoot = new();

    private static readonly ServiceProvider _serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();
    
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDatabaseUsage(services, configuration);
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
    
    private static void ConfigureDatabaseUsage(IServiceCollection services, IConfiguration configuration)
    {
        if (ShouldUseInMemoryDatabase())
        {
            ConfigureInMemoryDatabase(services);
        }
        else
        {
            ConfigureSqlServerDatabase(services, configuration);
        }
    }

    private static void ConfigureInMemoryDatabase(IServiceCollection services) =>
        services.AddPooledDbContextFactory<AuthenticatorDbContext>(options => options
            .UseInMemoryDatabase("InMemoryDatabase", _inMemoryDatabaseRoot)
            .UseInternalServiceProvider(_serviceProvider));

    private static void ConfigureSqlServerDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<AuthenticatorDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("authenticator"),
                builder => builder.MigrationsAssembly(typeof(AuthenticatorDbContext).Assembly.FullName)));
    }

    private static bool ShouldUseInMemoryDatabase()
    {
        string? useInMemoryDbVariable = Environment.GetEnvironmentVariable("IntegrationTestsUseInMemoryDb");
        return !string.IsNullOrEmpty(useInMemoryDbVariable) && string.Equals(useInMemoryDbVariable, "true",
            StringComparison.InvariantCultureIgnoreCase);
    }
}