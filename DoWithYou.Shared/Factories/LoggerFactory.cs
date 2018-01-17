using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DoWithYou.Shared.Factories
{
    public class LoggerFactory : ILoggerFactory
    {
        public Logger GetLogger()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Logs");
            string today = DateTime.Now.ToString("yyyyMMdd");

            var configuration = new LoggerConfiguration()
                .MinimumLevel?.Verbose()
                ?.MinimumLevel?.Override("Microsoft", LogEventLevel.Warning)
                ?.MinimumLevel?.Override("System", LogEventLevel.Warning)
                ?.Enrich?.FromLogContext()
                ?.WriteTo.Console()
                ?.WriteTo.Debug()
                ?.WriteTo.File(Path.Combine(filePath, $"{today}.log"));

            return configuration?.CreateLogger()
                ?? throw new ApplicationException($"Failed to generate a new {nameof(Logger)}");
        }

        public Logger GetLoggerFromConfiguration(IConfiguration config) => config == default(IConfiguration) ?
            GetLogger() :
            new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                ?.CreateLogger();
    }
}