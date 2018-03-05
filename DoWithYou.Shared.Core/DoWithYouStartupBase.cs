using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Core.Extensions;
using DoWithYou.Shared.Core.Middleware;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.Shared.Core
{
    public abstract class DoWithYouStartupBase : IDisposable
    {
        #region PROPERTIES
        public IContainer ApplicationContainer { get; set; }

        public IConfiguration Configuration { get; }

        protected IHostingEnvironment HostingEnvironment { get; }
        #endregion

        #region CONSTRUCTORS
        protected DoWithYouStartupBase(IConfiguration configuration, IHostingEnvironment env)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.SetupSerilogLogger(configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(DoWithYouStartupBase));

            Configuration = configuration;
            HostingEnvironment = env;
        }
        #endregion

        protected void Configure(IApplicationBuilder app)
        {
            ConfigureMiddleware(ref app);
            RegisterEvents(app.ApplicationServices.GetService<IApplicationLifetime>());
        }

        protected IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Adding Services to {Interface}", nameof(IServiceCollection));

            ApplicationContainer = services.BuildApplicationContainer(Configuration);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        protected void ConfigureMvc(ref IApplicationBuilder app) =>
            app.UseMvc();

        protected void ConfigureMvc(ref IApplicationBuilder app, Action<IRouteBuilder> routeBuilder) =>
            app.UseMvc(routeBuilder);

        protected void SetExceptionHandler(ref IApplicationBuilder app) =>
            app.UseDeveloperExceptionPage();

        protected void SetExceptionHandler(ref IApplicationBuilder app, string exceptionRoute) =>
            app.UseExceptionHandler(exceptionRoute);

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, nameof(DoWithYouStartupBase));

            ApplicationContainer?.Dispose();
            ApplicationContainer = null;
        }

        #region PRIVATE
        private void ConfigureMiddleware(ref IApplicationBuilder app)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Setting up {Interface} middleware.", nameof(IApplicationBuilder));

            // Logging for requests
            app.UseMiddleware<SerilogMiddleware>();
        }

        private void RegisterEvents(IApplicationLifetime applicationLifetime)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, LoggerTemplates.REGISTER_EVENT, nameof(ApplicationContainer), nameof(applicationLifetime.ApplicationStopped));
            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
        #endregion
    }
}