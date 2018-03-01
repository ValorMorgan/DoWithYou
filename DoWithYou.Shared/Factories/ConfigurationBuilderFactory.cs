using System;
using Microsoft.Extensions.Configuration;

namespace DoWithYou.Shared.Factories
{
    public class ConfigurationBuilderFactory : IConfigurationBuilderFactory
    {
        #region VARIABLES
        public const string DEVELOPMENT_ENVIRONMENT = "development";
        private const string JSON_EXTENSION = "json";
        private const string SETTING_FILE_NAME = "appsettings";
        #endregion
        
        public IConfigurationBuilder GetBuilder() => new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{SETTING_FILE_NAME}.{JSON_EXTENSION}", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        public IConfigurationBuilder GetBuilder(string basePath) => !string.IsNullOrWhiteSpace(basePath) ?
            GetBuilder().SetBasePath(basePath) :
            GetBuilder();

        public IConfigurationBuilder GetEnvironmentBuilder(string environment)
        {
            // Null / whitespace returns just the normal Builder
            if (string.IsNullOrWhiteSpace(environment))
                return GetBuilder();

            var builder = GetBuilder()
                .AddJsonFile($"{SETTING_FILE_NAME}.{environment}.{JSON_EXTENSION}", optional: true);

            if (environment.ToLower().Equals(DEVELOPMENT_ENVIRONMENT.ToLower()))
            {
                // TODO: Development specific configuration
            }

            return builder;
        }

        public IConfigurationBuilder GetEnvironmentBuilder(string environment, string basePath) => !string.IsNullOrWhiteSpace(basePath) ?
            GetEnvironmentBuilder(environment).SetBasePath(basePath) :
            GetEnvironmentBuilder(environment);
    }
}