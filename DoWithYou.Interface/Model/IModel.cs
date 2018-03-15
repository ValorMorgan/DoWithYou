using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
{
    public interface IModel
    {
    }

    public interface IModel<T1> : IModel
        where T1 : IBaseEntity
    {
    }

    public interface IModel<T1, T2> : IModel
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
    }
}