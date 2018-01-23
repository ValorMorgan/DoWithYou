using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
{
    public interface IModel<T1>
        where T1 : IBaseEntity
    {
    }

    public interface IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
    }
}