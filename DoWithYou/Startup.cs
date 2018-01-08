using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Service.Utilities;
using DoWithYou.Shared.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            ConfigureForEnvironment(ref app, env);
            ConfigureUsings(ref app);

            // Dispose resources resolved in container
            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutofac();

            IContainerBuilderFactory builderFactory = new ContainerBuilderFactory();
            var builder = builderFactory.GetBuilder();

            IContainerBuilderLayerFactory builderRegistry = new ContainerBuilderLayerFactory();
            builderRegistry.RegisterBuilderTypes(ref builder);

            // Register UI originated resources
            builder.RegisterInstance(Configuration);

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        #region PRIVATE
        private static void ConfigureForEnvironment(ref IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseStaticFiles();

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