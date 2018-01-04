using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Converters;
using Microsoft.Extensions.Configuration;

namespace DoWithYou.Shared.Repositories
{
    public class ApplicationSettings : IApplicationSettings
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        internal string selectedKey;
        #endregion

        #region PROPERTIES
        public string this[string key] => Configuration[key ?? selectedKey];

        public string this[params string[] path] => Configuration[JoinPathToKey(path) ?? selectedKey];

        private IConfiguration Configuration => _configuration ?? throw new NullReferenceException($"Cannot retrieve settings from a null {nameof(IConfiguration)} object.");
        #endregion

        #region CONSTRUCTORS
        public ApplicationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public T As<T>() =>
            As(typeof(T)) ?? default(T);

        public T As<T>(string key) =>
            As(typeof(T), key) ?? default(T);

        public T As<T>(params string[] path) =>
            As(typeof(T), JoinPathToKey(path)) ?? default(T);

        public dynamic As(Type type) =>
            As(type, default(string));

        public dynamic As(Type type, string key)
        {
            if (key != default)
                selectedKey = key;

            if (selectedKey == default)
                return default;

            string setting = Configuration[selectedKey];
            if (setting == default)
                return default;

            return Resolver.Resolve<IStringConverter>()?.Convert(setting)?.To(type) ?? default;
        }

        public dynamic As(Type type, params string[] path) =>
            As(type, JoinPathToKey(path));

        #region PRIVATE
        private string JoinPathToKey(IEnumerable<string> path) =>
            string.Join(":", path?.Where(p => p != default).Select(p => p.Trim()));
        #endregion
    }

    public static class ApplicationSettingsBuilder
    {
        public static IApplicationSettings Get(this IApplicationSettings applicationSettings, string key) =>
            new ApplicationSettings(Resolver.Resolve<IConfiguration>())
            {
                selectedKey = key
            };

        public static IApplicationSettings Get(this IApplicationSettings applicationSettings, params string[] path) =>
            new ApplicationSettings(Resolver.Resolve<IConfiguration>())
            {
                selectedKey = string.Join(":", path?.Where(p => p != default).Select(p => p.Trim()))
            };
    }
}