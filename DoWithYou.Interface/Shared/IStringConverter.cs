using System;

namespace DoWithYou.Interface.Shared
{
    public interface IStringConverter
    {
        T To<T>();

        dynamic To(Type type);
    }
}