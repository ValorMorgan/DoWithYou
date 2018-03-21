using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Entity.SQL;

namespace DoWithYou.Interface.Model
{
    public interface IModel
    {
    }

    public interface IModel<T1> : IModel
        where T1 : IBaseDocument
    {
    }

    public interface IModel<T1, T2> : IModel
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
    }
}