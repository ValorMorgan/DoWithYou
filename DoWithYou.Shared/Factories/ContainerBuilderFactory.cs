using System;
using Autofac;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Repositories;

namespace DoWithYou.Shared.Factories
{
    public class ContainerBuilderFactory : IContainerBuilderFactory
    {
        public ContainerBuilder GetBuilder()
        {
            // NOTE: We call Build early to expose any factories for instance setup
            var tempBuilder = new ContainerBuilder();
            RegisterTypesForBuilder(ref tempBuilder);

            var builder = new ContainerBuilder();
            RegisterTypesForBuilder(ref builder);
            RegisterInstancesForBuilder(tempBuilder.Build(), ref builder);

            return builder;
        }
        
        private static void RegisterInstancesForBuilder(IContainer container, ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // TODO: [Resolver] Setup factories

            builder.RegisterInstance<Serilog.ILogger>(container.Resolve<ILoggerFactory>().GetLogger());
        }

        private static void RegisterTypesForBuilder(ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<ApplicationSettings>()?.As<IApplicationSettings>();
            builder.RegisterType<StringConverter>()?.As<IStringConverter>();
            builder.RegisterType<LoggerFactory>()?.As<ILoggerFactory>();
        }
    }
}