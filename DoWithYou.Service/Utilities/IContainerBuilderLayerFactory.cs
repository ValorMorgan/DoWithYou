using Autofac;

namespace DoWithYou.Service.Utilities
{
    public interface IContainerBuilderLayerFactory
    {
        void RegisterBuilderLayerTypes(ref ContainerBuilder builder);
    }
}