using System;

namespace DoWithYou.Interface
{
    public interface IApplicationSettings
    {
        string this[string key] { get; }

        T As<T>();

        T As<T>(string key);

        dynamic As(Type type);

        dynamic As(Type type, string key);
    }
}
