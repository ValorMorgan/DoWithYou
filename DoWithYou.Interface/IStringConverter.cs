using System;

namespace DoWithYou.Interface
{
    public interface IStringConverter
    {
        T To<T>();

        dynamic To(Type type);
    }
}