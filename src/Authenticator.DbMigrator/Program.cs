using System.Diagnostics.CodeAnalysis;
using Authenticator.DbMigrator.Data;
using Authenticator.DbMigrator.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authenticator.DbMigrator;

/// <summary>
///     Provides for the application's hosting environment, configuration, and execution.
/// </summary>
[ExcludeFromCodeCoverage]
public class Program
{
    static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

    /// <summary>
    ///     Builds the application's host.
    /// </summary>
    /// <param name="args">A <see cref="string" />[] representing the application's arguments.</param>
    /// <returns>An <see cref="IHostBuilder" /> representing the host builder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddIdentitiesAndIdentityServer(GetConfigurationBuilder().Build());

                var provider = services.BuildServiceProvider();
                Seeds.InitializeIdentityDatabase(provider);
                Seeds.InitializeAuthDatabase(provider);

                Environment.Exit(0);
            });

    private static IConfigurationBuilder GetConfigurationBuilder() =>
        new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>().AddEnvironmentVariables();
}