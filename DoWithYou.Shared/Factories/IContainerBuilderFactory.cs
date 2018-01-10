using Autofac;
using Microsoft.Extensions.Configuration;

namespace DoWithYou.Shared.Factories
{
    public interface IContainerBuilderFactory
    {
        ContainerBuilder GetBuilder(IConfiguration config = null);
    }
}