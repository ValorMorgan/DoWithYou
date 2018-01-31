using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Core.Extensions;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Factories;
using DoWithYou.UI.Razor.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.UI.Razor
{
    public class Startup : IDisposable
    {
        #region PROPERTIES
        public IContainer ApplicationContainer { get; set; }

        public IConfiguration Configuration { get; }
        #endregion

        #region CONSTRUCTORS
        public Startup(IConfiguration configuration)
        {
            SetupLogger(configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(Startup));

            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            ConfigureForEnvironment(ref app, env);
            ConfigureMiddleware(ref app);
            RegisterEvents(ref applicationLifetime);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Adding Services to {Interface}", nameof(IServiceCollection));
            services.AddMvc().AddControllersAsServices();
            services.AddAutofac();

            ApplicationContainer = services.BuildApplicationContainer(Configuration);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Dispose()
        {
            ApplicationContainer?.Dispose();
            ApplicationContainer = null;
        }

        #region PRIVATE
        private static void ConfigureForEnvironment(ref IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Environment: {Environment}", env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                return;
            }

            app.UseExceptionHandler("/Error");
        }

        private static void ConfigureMiddleware(ref IApplicationBuilder app)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Setting up {Interface} middleware.", nameof(IApplicationBuilder));

            // Logging for requests
            app.UseMiddleware<SerilogMiddleware>();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }

        private void RegisterEvents(ref IApplicationLifetime applicationLifetime)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, LoggerTemplates.REGISTER_EVENT, nameof(ApplicationContainer), nameof(applicationLifetime.ApplicationStopped));
            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        private static void SetupLogger(IConfiguration configuration)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            Log.Logger = loggerFactory.GetLoggerFromConfiguration(configuration);
        }
        #endregion
    }
}