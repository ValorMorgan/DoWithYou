using Serilog.Core;

namespace DoWithYou.Shared.Factories
{
    public interface ILoggerFactory
    {
        Logger GetLogger();
    }
}