using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Service.Utilities;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Factories;
using DoWithYou.UI.Core.Shared.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.UI.Core.Shared
{
    public partial class StartupBase : Microsoft.AspNetCore.Hosting.StartupBase, IDisposable
    {
        #region PROPERTIES
        public IContainer ApplicationContainer { get; set; }

        public IConfiguration Configuration { get; }
        #endregion

        #region CONSTRUCTORS
        public StartupBase(IConfiguration configuration)
        {
            SetupLogger(configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(StartupBase));

            Configuration = configuration;
        }
        #endregion

        protected void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Adding Services to {Interface}", nameof(IServiceCollection));
        }

        public void Dispose()
        {
            ApplicationContainer?.Dispose();
            ApplicationContainer = null;
        }

        #region PRIVATE
        protected void ConfigureApplicationContainer(IServiceCollection services)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Generating {Class}", nameof(ContainerBuilder));
            IContainerBuilderFactory builderFactory = new ContainerBuilderFactory();
            var builder = builderFactory.GetBuilder(Configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Registering layer types to {Class}", nameof(ContainerBuilder));
            IContainerBuilderLayerFactory builderRegistry = new ContainerBuilderLayerFactory();
            builderRegistry.RegisterBuilderLayerTypes(ref builder);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Registering UI Instances to {Class}", nameof(ContainerBuilder));
            builder.RegisterInstance(Configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Populating services in {Class}", nameof(ContainerBuilder));
            builder.Populate(services);

            ApplicationContainer = builder.Build();
        }

        protected void ConfigureForEnvironment(ref IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Environment: {Environment}", env.EnvironmentName);
        }

        protected void ConfigureMiddleware(ref IApplicationBuilder app)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Setting up {Interface} middleware.", nameof(IApplicationBuilder));
        }

        protected void RegisterEvents(ref IApplicationLifetime applicationLifetime)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, LoggerTemplates.REGISTER_EVENT, nameof(ApplicationContainer), nameof(applicationLifetime.ApplicationStopped));
            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        protected static void SetupLogger(IConfiguration configuration)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            Log.Logger = loggerFactory.GetLoggerFromConfiguration(configuration);
        }
        #endregion
    }
}