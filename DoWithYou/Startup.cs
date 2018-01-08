using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Service.Utilities;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou
{
    public class Startup
    {
        #region PROPERTIES
        public IContainer ApplicationContainer { get; set; }

        public IConfiguration Configuration { get; }
        #endregion

        #region CONSTRUCTORS
        public Startup(IConfiguration configuration)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(Startup));

            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            ConfigureForEnvironment(ref app, env);
            ConfigureUsings(ref app);

            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering {Class} to {Event}", nameof(ApplicationContainer), nameof(applicationLifetime.ApplicationStopped));
            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Adding Services to {Interface}", nameof(IServiceCollection));
            services.AddMvc();
            services.AddAutofac();

            IContainerBuilderFactory builderFactory = new ContainerBuilderFactory();
            var builder = builderFactory.GetBuilder();

            IContainerBuilderLayerFactory builderRegistry = new ContainerBuilderLayerFactory();
            builderRegistry.RegisterBuilderTypes(ref builder);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Registering UI Instances to {Class}", nameof(ConfigurationBuilder));
            builder.RegisterInstance(Configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Populating services in {Class}", nameof(ConfigurationBuilder));
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        #region PRIVATE
        private static void ConfigureForEnvironment(ref IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Environment: {Environment}", env.EnvironmentName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
                app.UseExceptionHandler("/Error");
        }

        private static void ConfigureUsings(ref IApplicationBuilder app)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Setting {Interface} to Use Static Files", nameof(IApplicationBuilder));
            app.UseStaticFiles();

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Setting {Interface} to Use MVC Framework", nameof(IApplicationBuilder));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
        #endregion
    }
}