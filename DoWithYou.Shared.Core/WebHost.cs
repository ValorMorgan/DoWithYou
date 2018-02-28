using System;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace DoWithYou.Shared.Core
{
    public class WebHost : IDisposable
    {
        #region VARIABLES
        private IWebHost _host;
        #endregion

        #region CONSTRUCTORS
        public WebHost(string[] args, Type startupType)
        {
            _host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup(startupType)
                .UseSerilog()
                ?.Build();

            if (_host == default(IWebHost))
                throw new NullReferenceException($"Failed to build {nameof(IWebHost)} with provided {nameof(args)} and {nameof(startupType)}.");
        }
        #endregion

        public int Run()
        {
            int returnCode = 0;

            try
            {
                Log.Logger.LogEventInformation(LoggerEvents.STARTUP, "Starting web host");
                _host.Run();
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

        public void Dispose()
        {
            _host?.Dispose();
            _host = null;
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