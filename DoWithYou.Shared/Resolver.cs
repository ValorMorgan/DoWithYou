using System;
using Autofac;

namespace DoWithYou.Shared
{
    public class Resolver : IDisposable
    {
        #region VARIABLES
        private IContainer _container;
        #endregion

        #region PROPERTIES
        public IContainer Container => _container ?? (_container = GetContainer());
        #endregion

        public void Dispose()
        {
            _container?.Dispose();
            _container = null;
        }

        public bool IsRegistered<T>() => Container.IsRegistered<T>();

        // NOTE: Resolve will return default(T) when the desired T is NOT registered (instead of throwing an exception)
        public T Resolve<T>() => IsRegistered<T>() ? Container.Resolve<T>() : default;

        #region PRIVATE
        private ContainerBuilder GetBuilder()
        {
            var builder = new ContainerBuilder();

            // TODO: [Resolver] setup base registrations

            return builder;
        }

        private ContainerBuilder GetBuilderWithInstances()
        {
            // NOTE: We call GetBuilder early to resolve from our typical setup
            var tempContainer = GetBuilder().Build();

            // TODO: [Resolver] Setup factories

            // NOTE: Called again to produce the new final builder (can be a performance impact)
            var builder = GetBuilder();

            // TODO: [Resolver] Register factory creations

            return builder;
        }

        private IContainer GetContainer() => GetBuilder().Build();
        #endregion
    }
}