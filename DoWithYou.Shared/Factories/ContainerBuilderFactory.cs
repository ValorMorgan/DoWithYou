using System;
using Autofac;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Repositories;
using Serilog;

namespace DoWithYou.Shared.Factories
{
    public class ContainerBuilderFactory : IContainerBuilderFactory
    {
        public ContainerBuilder GetBuilder()
        {
            Log.Logger.LogEventInformation(LoggerEvents.LIBRARY, "Constructing {Class}.", nameof(ContainerBuilder));

            // NOTE: We call Build early to expose any factories for instance setup
            var tempBuilder = new ContainerBuilder();
            RegisterTypesForBuilder(ref tempBuilder);

            var builder = new ContainerBuilder();
            RegisterTypesForBuilder(ref builder);
            RegisterInstancesForBuilder(tempBuilder.Build(), ref builder);

            Log.Logger.LogEventInformation(LoggerEvents.LIBRARY, "{Class} constructed.", nameof(ContainerBuilder));
            return builder;
        }

        #region PRIVATE
        private static void RegisterInstancesForBuilder(IContainer container, ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterInstance<ILogger>(container.Resolve<ILoggerFactory>().GetLogger());
        }

        private static void RegisterTypesForBuilder(ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // Alphabetical order on Class name
            builder.RegisterType<ApplicationSettings>()?.As<IApplicationSettings>();
            builder.RegisterType<LoggerFactory>()?.As<ILoggerFactory>();
            builder.RegisterType<StringConverter>()?.As<IStringConverter>();
        }
        #endregion
    }
}