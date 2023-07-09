using System.Diagnostics.CodeAnalysis;
using Authenticator.Domain.Entities;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Newtonsoft.Json;

namespace Authenticator.DbMigrator.Data;

/// <summary>
/// In memory authenticator config. Provides default seed data client, scopes and resources.
/// </summary>
[ExcludeFromCodeCoverage]
public class AuthenticatorConfigData
{
    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope> { new(name: "api", displayName: "Access apis") };
    
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new("api")
            {
                Scopes = { "api" },
                ApiSecrets = { new Secret("authenticator_resource_owner_flow_secret".Sha256()) }
            }
        };
    }
    
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new()
            {
                ClientName = "Authenticator Resource Owner Flow",
                ClientId = "AuthenticatorResourceOwnerClientId",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("authenticator_resource_owner_flow_secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "api"
                },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AccessTokenLifetime = 300, //5min
                IdentityTokenLifetime = 300, //5min
                SlidingRefreshTokenLifetime = 7200, //2h
                AbsoluteRefreshTokenLifetime = 10800 //3h
            }
        };
    }
    
    public static IEnumerable<Country> GetCountries()
    {
        var path = Path.GetFullPath("Countries.json");
        var fileContent = File.ReadAllText(path);
        var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(fileContent);

        return countries;
    }
}