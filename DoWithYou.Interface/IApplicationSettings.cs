using System;

namespace DoWithYou.Interface
{
    public interface IApplicationSettings
    {
        T As<T>();

        T As<T>(string key);

        T As<T>(params string[] path);

        dynamic As(Type type);

        dynamic As(Type type, string key);

        dynamic As(Type type, params string[] path);

        string this[string key] { get; }

        string this[params string[] path] { get; }
    }
}