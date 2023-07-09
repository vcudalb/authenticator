using System.Diagnostics.CodeAnalysis;
using Authenticator.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Authenticator.Api;

    /// <summary>
    ///     Provides operations in order to configure the components of the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        ///  A <see cref="string"/> represents swagger base path. Please ammend and launch options in case of changing value.
        /// </summary>
        private readonly string _swaggerBasePath;

        /// <summary>
        ///     A <see cref="IWebHostEnvironment"/>  provides information about the web hosting environment an application is running in.
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        ///     Constructs a new instance of <see cref="Startup" />.
        /// </summary>
        /// <param name="configuration">
        ///     A <see cref="IConfiguration"/> represents a set of key/value application configuration properties.
        /// </param>
        /// <param name="webHostEnvironment">
        ///     A <see cref="IWebHostEnvironment"/>  provides information about the web hosting environment an application is running in.
        /// </param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _swaggerBasePath = "swagger/authenticator";
        }

        /// <summary>
        ///     A <see cref="IConfiguration"/> represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        ///     A <see cref="IServiceCollection"/> specifies the contract for a collection of service descriptors.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDuendeIndentityServer(Configuration);
        }
        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        ///     A <see cref="IApplicationBuilder"/>  defines a class that provides the mechanisms to configure an application's request pipeline.
        /// </param>
        /// <param name="env">
        ///     A <see cref="IWebHostEnvironment"/>  provides information about the web hosting environment an application is running in.
        /// </param>
        /// <param name="provider"> 
        ///     A <see cref="IApiVersionDescriptionProvider"/> defines the behavior of a provider that discovers and describes API version information within an application.
        /// </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env /*, IApiVersionDescriptionProvider provider*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }