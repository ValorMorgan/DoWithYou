using System;
using System.IO;
using Autofac;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Repositories;
using Serilog;

namespace DoWithYou.Shared
{
    static class ResolverRegistry
    {
        public static void RegisterTypesForBuilder(ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<ApplicationSettings>()?.As<IApplicationSettings>();
            builder.RegisterType<StringConverter>()?.As<IStringConverter>();
        }

        public static void RegisterInstancesForBuilder(IContainer container, ref ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // TODO: [Resolver] Setup factories
            
            builder.RegisterInstance<ILogger>(new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    path: Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"{DateTime.Now.ToShortDateString()}.log"),
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .CreateLogger());
        }
    }
}
