using System;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace DoWithYou.UI.React
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
            int returnCode = 0;

            try
            {
                // Log.Logger not setup until after we build the WebHost
                using (IWebHost host = BuildWebHost(args))
                {
                    Log.Logger.LogEventInformation(LoggerEvents.STARTUP, "Starting web host");
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                if (Log.Logger != null)
                    Log.Logger.LogEventFatal(ex, LoggerEvents.SHUTDOWN, "Host terminated unexpectedly");

                returnCode = 1;
            }
            finally
            {
                if (!TryCloseAndFlushLogger())
                    returnCode = 1;
            }

            return returnCode;
        }

        #region PRIVATE
        private static bool TryCloseAndFlushLogger()
        {
            if (Log.Logger == null)
                return true;

            try
            {
                Log.Logger.LogEventInformation(LoggerEvents.SHUTDOWN, "Closing and Flushing logger.");
                Log.CloseAndFlush();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}