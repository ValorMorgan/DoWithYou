using System;
using DoWithYou.Interface;
using DoWithYou.Shared.Converters;
using Microsoft.Extensions.Configuration;

namespace DoWithYou.Shared.Repositories
{
    public class ApplicationSettings : IApplicationSettings
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        internal string extensionKeyProvided;
        #endregion

        #region PROPERTIES
        public string this[string key] => _configuration[key];
        #endregion

        #region CONSTRUCTORS
        public ApplicationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public T As<T>() =>
            As(typeof(T));

        public T As<T>(string key) =>
            As(typeof(T), key);

        public dynamic As(Type type) =>
            As(type, default);

        public dynamic As(Type type, string key)
        {
            if (key != default)
                extensionKeyProvided = key;

            if (extensionKeyProvided == default)
                throw new InvalidOperationException("Setting key is required.");

            return Resolver.Resolve<IStringConverter>().Convert(extensionKeyProvided)?.To(type) ?? default;
        }
    }

    public static class ApplicationSettingsBuilder
    {
        public static IApplicationSettings Get(this IApplicationSettings applicationSettings, string key) =>
            new ApplicationSettings(Resolver.Resolve<IConfiguration>()) {extensionKeyProvided = key};
    }
}