using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Authenticator.Infrastructure.Persistence.Contexts;

namespace Authenticator.DbMigrator.Data;

/// <summary>
///     Db seeds provider service.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Seeds
{
    public static void InitializeIdentityDatabase(IServiceProvider serviceProvider)
    {
        using (var context = serviceProvider.GetRequiredService<PersistedGrantDbContext>())
        {
            context.Database.Migrate();

            Console.WriteLine($"{nameof(PersistedGrantDbContext)} database migrated successfully");
        }

        using (var context = serviceProvider.GetRequiredService<ConfigurationDbContext>())
        {
            context.Database.Migrate();

            SeedClients(context, AuthenticatorConfigData.GetClients());
            SeedApiResources(context, AuthenticatorConfigData.GetApiResources());
            SeedApiScope(context, AuthenticatorConfigData.GetApiScopes());

            Console.WriteLine($"{nameof(ConfigurationDbContext)} database migrated successfully");
        }
    }

    public static void InitializeAuthDatabase(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<AuthenticatorDbContext>();
        context.Database.Migrate();
        SeedCountries(context, AuthenticatorConfigData.GetCountries());

        Console.WriteLine($"{nameof(AuthenticatorDbContext)} database migrated successfully");
    }

    private static void SeedClients(ConfigurationDbContext context, IEnumerable<Client> clients)
    {
        if (!context.Clients.Any())
        {
            foreach (var client in clients)
            {
                context.Clients.Add(client.ToEntity());
            }

            context.SaveChanges();
        }
        else
        {
            foreach (var client in clients)
            {
                var dbClient = context.Clients.FirstOrDefault(c => c.ClientId == client.ClientId);
                if (dbClient is not null)
                {
                    dbClient.AccessTokenLifetime = client.AccessTokenLifetime;
                    dbClient.RefreshTokenExpiration = (int)client.RefreshTokenExpiration;
                    dbClient.SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime;
                    dbClient.AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime;
                }
            }

            context.SaveChanges();
        }
    }

    private static void SeedApiResources(ConfigurationDbContext context, IEnumerable<ApiResource> apiResources)
    {
        if (context.ApiResources.Any()) return;
        foreach (var apiResource in apiResources)
        {
            context.ApiResources.Add(apiResource.ToEntity());
        }

        context.SaveChanges();
    }

    private static void SeedApiScope(ConfigurationDbContext context, IEnumerable<ApiScope> apiScopes)
    {
        if (context.ApiScopes.Any()) return;
        foreach (var apiScope in apiScopes)
        {
            context.ApiScopes.Add(apiScope.ToEntity());
        }

        context.SaveChanges();
    }

    private static void SeedCountries(AuthenticatorDbContext context, IEnumerable<Country> countries)
    {
        if (context.Countries.Any()) return;
        foreach (var country in countries)
        {
            country.CreatedOn = DateTime.UtcNow;
            country.UpdatedOn = DateTime.UtcNow;
        }

        context.Countries.AddRange(countries);
        context.SaveChanges();
    }
}