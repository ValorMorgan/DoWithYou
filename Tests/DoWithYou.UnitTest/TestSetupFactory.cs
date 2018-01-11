using System.IO;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Repositories;
using DoWithYou.Shared.Repositories.Settings;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DoWithYou.UnitTest
{
    static class TestSetupFactory
    {
        #region PRIVATE
        private static bool DoesAppSettingsFileExist() =>
            File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        #endregion

        internal static IConfiguration GetApplicationSettingsConfiguration()
        {
            // Validate file exists (needed for tests to work)
            if (!DoesAppSettingsFileExist())
                Assert.Inconclusive();
            
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                ?.Build();
        }

        internal static ILoggerTemplates GetLoggerTemplates()
        {
            IConfiguration configuration = GetApplicationSettingsConfiguration();
            AppConfig appConfig = configuration.Get<AppConfig>();
            return new LoggerTemplates(appConfig);
        }
    }
}