using Autofac;

namespace DoWithYou.Shared.Factories
{
    public interface IContainerBuilderFactory
    {
        ContainerBuilder GetBuilder();
    }
}