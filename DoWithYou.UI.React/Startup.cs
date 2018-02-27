using System;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Core;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.UI.React
{
    public class Startup : DoWithYouStartupBase, IStartup
    {
        #region CONSTRUCTORS
        public Startup(IConfiguration configuration, IHostingEnvironment env)
            : base(configuration, env)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(Startup));
        }
        #endregion

        public new void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            ConfigureForEnvironment(ref app);

            ConfigureMvc(ref app, routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Index"
                    });
            });
        }

        public new IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();

            return base.ConfigureServices(services);
        }

        #region PRIVATE
        private void ConfigureForEnvironment(ref IApplicationBuilder app)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Environment: {Environment}", HostingEnvironment.EnvironmentName);

            // Every environment
            app.UseStaticFiles();
            
            if (HostingEnvironment.IsDevelopment())
                SetupAppForDevelopment(ref app);
            else if (HostingEnvironment.IsProduction())
                SetupAppForProduction(ref app);
        }

        private void SetupAppForDevelopment(ref IApplicationBuilder app)
        {
            SetExceptionHandler(ref app);

            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                }
            );
        }

        private void SetupAppForProduction(ref IApplicationBuilder app)
        {
            SetExceptionHandler(ref app, "/Home/Error");
        }
        #endregion
    }
}