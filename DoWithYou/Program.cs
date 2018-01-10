using System;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
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
            try
            {
                // Log.Logger not setup until after we build the WebHost
                IWebHost host = BuildWebHost(args);

                Log.Logger.LogEventInformation(LoggerEvents.STARTUP, "Starting web host");
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                if (Log.Logger != null)
                    Log.Logger.LogEventFatal(ex, LoggerEvents.SHUTDOWN, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                if (Log.Logger != null)
                {
                    Log.Logger.LogEventInformation(LoggerEvents.SHUTDOWN, "Closing and Flushing logger.");
                    Log.CloseAndFlush();
                }
            }
        }
    }
}