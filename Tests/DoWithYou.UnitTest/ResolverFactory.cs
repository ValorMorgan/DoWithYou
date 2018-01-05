using System.IO;
using DoWithYou.Shared;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DoWithYou.UnitTest
{
    static class ResolverFactory
    {
        #region PRIVATE
        private static bool DoesAppSettingsFileExist() =>
            File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        #endregion

        internal static void SetupResolverForTesting()
        {
            // Validate file exists (needed for tests to work)
            if (!DoesAppSettingsFileExist())
                Assert.Inconclusive();

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());

            IConfiguration configuration = builder
                .AddJsonFile("appsettings.json")
                ?.Build();

            Resolver.InitializeContainerWithConfiguration(configuration);
        }
    }
}