using System;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Shared.Converters
{
    public class StringConverter : IStringConverter
    {
        #region VARIABLES
        private readonly string _toConvert;
        #endregion

        #region CONSTRUCTORS
        public StringConverter()
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.Constructor, nameof(StringConverter));

            _toConvert = default;
        }

        internal StringConverter(string value)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, LoggerTemplates.Constructor, nameof(StringConverter));

            _toConvert = value;
        }
        #endregion

        public T To<T>() =>
            To(typeof(T)) ?? default(T);

        public dynamic To(Type type)
        {
            if (_toConvert == default || type == default)
                return default;

            Log.Logger.LogEventInformation(LoggerEvents.LIBRARY, LoggerTemplates.ConvertTo, _toConvert, type.FullName);

            // Try and let System.ComponentModel.StringConverter do the conversion
            var converter = new System.ComponentModel.StringConverter();

            return converter.CanConvertTo(type) ?
                converter.ConvertTo(_toConvert, type) :
                GetStringConvertedToType(type);
        }

        #region PRIVATE
        private dynamic GetStringConvertedToType(Type type)
        {
            Log.Logger.LogEventInformation(LoggerEvents.LIBRARY, "Attempting manual conversion of {Value} to type {Type}", _toConvert, type.FullName);

            switch (type)
            {
                case Type _ when type == typeof(string):
                    return _toConvert;

                case Type _ when type == typeof(char):
                    return char.TryParse(_toConvert, out char convertedToChar) ?
                        convertedToChar : default;

                case Type _ when type == typeof(bool):
                    return bool.TryParse(_toConvert, out bool convertedToBool) ?
                        convertedToBool : default;

                case Type _ when type == typeof(byte):
                    return byte.TryParse(_toConvert, out byte convertedToByte) ?
                        convertedToByte : default;

                case Type _ when type == typeof(sbyte):
                    return sbyte.TryParse(_toConvert, out sbyte convertedToSByte) ?
                        convertedToSByte : default;

                case Type _ when type == typeof(decimal):
                    return decimal.TryParse(_toConvert, out decimal convertedToDecimal) ?
                        convertedToDecimal : default;

                case Type _ when type == typeof(double):
                    return double.TryParse(_toConvert, out double convertedToDouble) ?
                        convertedToDouble : default;

                case Type _ when type == typeof(float):
                    return float.TryParse(_toConvert, out float convertedToFloat) ?
                        convertedToFloat : default;

                case Type _ when type == typeof(int):
                    return int.TryParse(_toConvert, out int convertedToInt) ?
                        convertedToInt : default;

                case Type _ when type == typeof(uint):
                    return uint.TryParse(_toConvert, out uint convertedToUInt) ?
                        convertedToUInt : default;

                case Type _ when type == typeof(long):
                    return long.TryParse(_toConvert, out long convertedToLong) ?
                        convertedToLong : default;

                case Type _ when type == typeof(ulong):
                    return ulong.TryParse(_toConvert, out ulong convertedToULong) ?
                        convertedToULong : default;

                case Type _ when type == typeof(short):
                    return short.TryParse(_toConvert, out short convertedToShort) ?
                        convertedToShort : default;

                case Type _ when type == typeof(ushort):
                    return ushort.TryParse(_toConvert, out ushort convertedToUShort) ?
                        convertedToUShort : default;
            }

            Log.Logger.LogEventWarning(LoggerEvents.LIBRARY, "Manual conversion of {Value} to type {Type} failed. Returning default(string).", _toConvert, type.FullName);
            return default;
        }
        #endregion
    }

    public static class StringConverterBuilder
    {
        public static IStringConverter Convert(this IStringConverter converter, string value) =>
            new StringConverter(value);
    }
}