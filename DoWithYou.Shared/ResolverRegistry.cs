using System;
using Autofac;
using DoWithYou.Interface;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Repositories;

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

            // TODO: [Resolver] Register factory creations
        }
    }
}
