using System;
using Autofac;

namespace DoWithYou.Shared
{
    public class Resolver : IDisposable
    {
        #region VARIABLES
        private static IContainer _container;
        #endregion

        #region PROPERTIES
        public static IContainer Container => _container ?? (_container = GetContainer());
        #endregion

        public void Dispose()
        {
            _container?.Dispose();
            _container = null;
        }

        public static bool IsRegistered<T>() => Container.IsRegistered<T>();

        // NOTE: Resolve will return default(T) when the desired T is NOT registered (instead of throwing an exception)
        public static T Resolve<T>() => IsRegistered<T>() ? Container.Resolve<T>() : default;

        #region PRIVATE
        private static ContainerBuilder GetBuilder()
        {
            var builder = new ContainerBuilder();

            // TODO: [Resolver] setup base registrations

            return builder;
        }

        private static ContainerBuilder GetBuilderWithInstances()
        {
            // NOTE: We call GetBuilder early to resolve from our typical setup
            var tempContainer = GetBuilder().Build();

            // TODO: [Resolver] Setup factories

            // NOTE: Called again to produce the new final builder (can be a performance impact)
            var builder = GetBuilder();

            // TODO: [Resolver] Register factory creations

            return builder;
        }

        private static IContainer GetContainer() => GetBuilder().Build();
        #endregion
    }
}