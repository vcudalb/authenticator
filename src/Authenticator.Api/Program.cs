﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
            await CreateHostBuilder(args).Build().RunAsync();
        }
     
        /// <summary>
        ///     Runs the application.
        /// </summary>
        /// <param name="args">
        ///     A <see cref="string" />[] representing the application's arguments.
        /// </param>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}