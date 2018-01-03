using System;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace DoWithYou.Shared
{
    public class Resolver : IDisposable
    {
        #region VARIABLES
        private static IContainer CONTAINER;
        #endregion

        #region PROPERTIES
        public static IContainer Container => CONTAINER ?? (CONTAINER = GetContainer());
        #endregion

        public static void InitializeContainerWithConfiguration(IConfiguration configuration)
        {
            var builder = GetBuilder();

            builder.RegisterInstance(configuration);

            CONTAINER = builder.Build();
        }

        public static bool IsRegistered<T>() => Container.IsRegistered<T>();
        
        // NOTE: Resolve will return default(T) when the desired T is NOT registered (instead of throwing an exception)
        public static T Resolve<T>() => IsRegistered<T>() ? Container.Resolve<T>() : default;

        public void Dispose()
        {
            CONTAINER?.Dispose();
            CONTAINER = null;
        }

        #region PRIVATE
        private static IContainer GetContainer() => GetBuilder().Build();

        private static ContainerBuilder GetBuilder()
        {
            // NOTE: We call Build early to expose any factories for instance setup
            var tempBuilder = new ContainerBuilder();
            ResolverRegistry.RegisterTypesForBuilder(ref tempBuilder);
            var tempContainer = tempBuilder.Build();

            var builder = new ContainerBuilder();
            ResolverRegistry.RegisterTypesForBuilder(ref builder);
            ResolverRegistry.RegisterInstancesForBuilder(tempContainer, ref builder);

            return builder;
        }
        #endregion
    }
}