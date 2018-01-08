using System;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Factories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace DoWithYou
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();

        public static int Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            Log.Logger = loggerFactory.GetLogger();

            try
            {
                Log.Logger.LogEventInformation(LoggerEvents.STARTUP, "Starting web host");
                BuildWebHost(args)
                    .Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Logger.LogEventFatal(ex, LoggerEvents.SHUTDOWN, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.Logger.LogEventInformation(LoggerEvents.SHUTDOWN, "Closing and Flushing logger.");
                Log.CloseAndFlush();
            }
        }
    }
}