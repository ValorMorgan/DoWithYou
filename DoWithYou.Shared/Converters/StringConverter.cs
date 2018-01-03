using System;
using DoWithYou.Interface;

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
            _toConvert = default;
        }

        public StringConverter(string value)
        {
            _toConvert = value;
        }
        #endregion

        public T To<T>() =>
            To(typeof(T)) ?? default(T);

        public dynamic To(Type type)
        {
            if (_toConvert == default)
                return default;

            dynamic converted;

            switch (type)
            {
                case Type _ when type == typeof(string):
                    converted = _toConvert;
                    break;
                case Type _ when type == typeof(char):
                    converted = char.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(bool):
                    converted = bool.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(byte):
                    converted = byte.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(sbyte):
                    converted = sbyte.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(decimal):
                    converted = decimal.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(double):
                    converted = double.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(float):
                    converted = float.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(int):
                    converted = int.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(uint):
                    converted = uint.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(long):
                    converted = long.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(ulong):
                    converted = ulong.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(short):
                    converted = short.Parse(_toConvert);
                    break;
                case Type _ when type == typeof(ushort):
                    converted = ushort.Parse(_toConvert);
                    break;
                default:
                    throw new InvalidCastException($"Cannot convert provided string to type \"{type.Name}\".");
            }

            return converted;
        }
    }

    public static class StringConverterBuilder
    {
        public static IStringConverter Convert(this IStringConverter converter, string value) =>
            new StringConverter(value);
    }
}