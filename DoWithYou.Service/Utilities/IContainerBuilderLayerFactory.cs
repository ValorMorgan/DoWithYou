using Autofac;

namespace DoWithYou.Service.Utilities
{
    public interface IContainerBuilderLayerFactory
    {
        void RegisterBuilderTypes(ref ContainerBuilder builder);
    }
}