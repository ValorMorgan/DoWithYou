using System;
using Autofac;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Repositories.Settings;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DoWithYou.Shared.Factories
{
    public class ContainerBuilderFactory : IContainerBuilderFactory
    {
        public ContainerBuilder GetBuilder(IConfiguration config = null)
        {
            Log.Logger.LogEventDebug(LoggerEvents.LIBRARY, "Constructing {Class}.", nameof(ContainerBuilder));

            // NOTE: We call Build early to expose any factories for instance setup
            var tempBuilder = new ContainerBuilder();
            RegisterTypesForBuilder(ref tempBuilder);

            var builder = new ContainerBuilder();
            RegisterTypesForBuilder(ref builder);
            RegisterInstancesForBuilder(ref builder, tempBuilder.Build(), config);

            Log.Logger.LogEventDebug(LoggerEvents.LIBRARY, "{Class} constructed.", nameof(ContainerBuilder));
            return builder;
        }

        #region PRIVATE
        private static void RegisterInstancesForBuilder(ref ContainerBuilder builder, IContainer container, IConfiguration config)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // Logger
            ILoggerFactory loggerFactory = container.Resolve<ILoggerFactory>();
            builder.RegisterInstance<ILogger>(loggerFactory.GetLoggerFromConfiguration(config));

            // Settings
            builder.RegisterInstance(config.Get<AppConfig>());
        }

        private static void RegisterTypesForBuilder(ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // Alphabetical order on Class name
            builder.RegisterType<LoggerFactory>()?.As<ILoggerFactory>();
            builder.RegisterType<StringConverter>()?.As<IStringConverter>();
        }
        #endregion
    }
}