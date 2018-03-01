using System;
using System.Collections.Generic;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
            try
            {
                // TODO: Cleanup Configuration setup to get InMemory first
                _host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddInMemoryCollection(GetInMemorySettings());
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                        config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                        config.AddEnvironmentVariables();
                    })
                    .UseStartup(startupType)
                    .UseSerilog()
                    ?.Build();

                if (_host == default(IWebHost))
                    throw new NullReferenceException($"{nameof(IWebHost)} built as a default{nameof(IWebHost)}.");
            }
            catch (Exception ex)
            {
                var wrapper = new ApplicationException($"Failed to build {nameof(IWebHost)} with provided {nameof(args)} and {nameof(startupType)}.", ex);

                // TODO: Log error somehow (global Logger may be null at this point).
                Console.WriteLine(wrapper.ToString());

                throw wrapper;
            }
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
        private static IEnumerable<KeyValuePair<string, string>> GetInMemorySettings() =>
            new Dictionary<string, string>
            {
                {"ConnectionStrings:0:Name", "DoWithYou"},
                {"ConnectionStrings:0:Connection", "Server=(localdb)\\v11.0;Initial Catalog=DoWithYouDB;Integrated Security=true;"},
                {"Logging:LogLevel:Default", "Warning"},
                {"Serilog:MinimumLevel:Default", "Verbose"},
                {"Serilog:MinimumLevel:Override:Microsoft", "Warning"},
                {"Serilog:MinimumLevel:Override:System", "Warning"},
                {"Serilog:Enrich:0", "FromLogContext"},
                {"Serilog:WriteTo:0", "Console"},
                {"Serilog:WriteTo:1", "Debug"},
                {"Serilog:WriteTo:2:Name", "RollingFile"},
                {"Serilog:WriteTo:2:Args:pathFormat", ".\\Logs\\{Date}.log"},
                {"Serilog:WriteTo:2:Args:outputTemplate", "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}"}
            };

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