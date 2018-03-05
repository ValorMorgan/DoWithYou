using System;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Core;
using DoWithYou.Shared.Core.Middleware;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.API
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
            app.UseMiddleware<BadResponseMiddleware>();

            base.Configure(app);

            base.ConfigureMvc(ref app);

            ConfigureForEnvironment(ref app);
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
            
            if (HostingEnvironment.IsDevelopment())
                SetupAppForDevelopment(ref app);
            else if (HostingEnvironment.IsProduction())
                SetupAppForProduction(ref app);
        }

        private void SetupAppForDevelopment(ref IApplicationBuilder app)
        {
            SetExceptionHandler(ref app);
        }

        private void SetupAppForProduction(ref IApplicationBuilder app)
        {
            SetExceptionHandler(ref app, "/Home/Error");
        }
        #endregion
    }
}