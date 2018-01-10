using System;
using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DoWithYou.Shared.Factories
{
    public class LoggerFactory : ILoggerFactory
    {
        public Logger GetLogger()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            string today = DateTime.Now.ToString("yyyyMMdd");

            // TODO: Serilog -> Settings to appsettings.json file
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    path: Path.Combine(filePath, $"{today}.log"))
                .WriteTo.File(
                    path: Path.Combine(filePath, $"{today}_INFO.log"),
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(
                    path: Path.Combine(filePath, $"{today}_ERROR.log"),
                    restrictedToMinimumLevel: LogEventLevel.Error)
                .CreateLogger();
        }
    }
}