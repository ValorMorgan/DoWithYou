using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DoWithYou.Shared.Repositories
{
    public class ApplicationSettings : IApplicationSettings
    {
        #region VARIABLES
        private readonly IConfiguration _configuration;
        private string _selectedKey;
        #endregion

        #region PROPERTIES
        public string this[string key]
        {
            get
            {
                Log.Logger.LogEventVerbose(LoggerEvents.LIBRARY, "Getting setting \"{Key}\"", key);
                return Configuration[key ?? SelectedKey];
            }
        }

        public string this[params string[] path] => this[JoinPathToKey(path) ?? SelectedKey];

        internal IConfiguration Configuration => _configuration ?? throw new NullReferenceException($"Cannot retrieve settings from a null {nameof(IConfiguration)} object.");

        internal IStringConverter Converter { get; }

        internal string SelectedKey
        {
            get => _selectedKey ?? string.Empty;
            set => _selectedKey = value;
        }
        #endregion

        #region CONSTRUCTORS
        public ApplicationSettings(IConfiguration configuration, IStringConverter converter)
        {
            Log.Logger.LogEventInformation(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(ApplicationSettings));

            _configuration = configuration;
            Converter = converter;
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
            Log.Logger.LogEventVerbose(LoggerEvents.LIBRARY, "Getting setting \"{Key}\" as type {Type}", key, type?.FullName ?? "null");

            if (key == default)
            {
                if (_selectedKey == default)
                    return default;

                key = _selectedKey;
            }

            string setting = Configuration[key];
            if (setting == default)
                return default;

            return Converter.Convert(setting)?.To(type) ?? default;
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
            GetApplicationSettings(applicationSettings, key);

        public static IApplicationSettings Get(this IApplicationSettings applicationSettings, params string[] path) =>
            Get(applicationSettings, string.Join(":", path?.Where(p => p != default).Select(p => p.Trim())));

        #region PRIVATE
        private static ApplicationSettings GetApplicationSettings(IApplicationSettings settings, string key)
        {
            var config = ((ApplicationSettings)settings).Configuration;
            var converter = ((ApplicationSettings)settings).Converter;
            return new ApplicationSettings(config, converter)
            {
                SelectedKey = key
            };
        }
        #endregion
    }
}