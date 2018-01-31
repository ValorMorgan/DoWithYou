using Autofac;
using Autofac.Extensions.DependencyInjection;
using DoWithYou.Service.Utilities;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DoWithYou.Shared.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IContainer BuildApplicationContainer(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Generating {Class}", nameof(ContainerBuilder));
            IContainerBuilderFactory builderFactory = new ContainerBuilderFactory();
            var builder = builderFactory.GetBuilder(configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Registering layer types to {Class}", nameof(ContainerBuilder));
            IContainerBuilderLayerFactory builderRegistry = new ContainerBuilderLayerFactory();
            builderRegistry.RegisterBuilderLayerTypes(ref builder);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Registering UI Instances to {Class}", nameof(ContainerBuilder));
            builder.RegisterInstance(configuration);

            Log.Logger.LogEventVerbose(LoggerEvents.STARTUP, "Populating services in {Class}", nameof(ContainerBuilder));
            builder.Populate(services);

            return builder.Build();
        }
    }
}