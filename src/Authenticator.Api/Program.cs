using System.Diagnostics.CodeAnalysis;

namespace Authenticator.Api
{
    /// <summary>
    ///     Provides for the application's hosting environment, configuration, and execution.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        ///     Provides the application's main entry point.
        /// </summary>
        /// <param name="args">
        ///  A <see cref="string" />[] representing the application's arguments.
        /// </param>
        public static async Task Main(string[] args)
        {
            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex) when (
                ex.GetType().Name is not "StopTheHostException"
                && ex.GetType().Name is not "HostAbortedException")
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}