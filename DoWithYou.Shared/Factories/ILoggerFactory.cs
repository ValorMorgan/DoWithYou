using Microsoft.Extensions.Configuration;
using Serilog.Core;

namespace DoWithYou.Shared.Factories
{
    public interface ILoggerFactory
    {
        Logger GetLogger();
        Logger GetLoggerFromConfiguration(IConfiguration config);
        void SetupSerilogLogger(IConfiguration configuration);
    }
}