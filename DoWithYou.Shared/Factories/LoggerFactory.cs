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
        [Obsolete("Should use <cref=\"GetLoggerFromConfiguration\"> which generates based on provided settings.")]
        public Logger GetLogger()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            string today = DateTime.Now.ToString("yyyyMMdd");

            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    path: Path.Combine(filePath, $"{today}.log"))
                .CreateLogger();
        }

        public Logger GetLoggerFromConfiguration(IConfiguration config) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
    }
}