using System;
using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DoWithYou.Shared.Factories
{
    public class LoggerFactory : ILoggerFactory
    {
        public Logger GetLogger() =>
            new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    path: Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"{DateTime.Now.ToShortDateString()}.log"),
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();
    }
}