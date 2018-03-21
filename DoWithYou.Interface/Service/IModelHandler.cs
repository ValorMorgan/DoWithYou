using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Entity.SQL;
using DoWithYou.Interface.Model;

namespace DoWithYou.Interface.Service
{
    public interface IModelHandler<TModel, T1> : IModelRepository<TModel, T1>
        where TModel : IModel<T1>
        where T1 : IBaseDocument
    {
    }

    public interface IModelHandler<TModel, T1, T2> : IModelRepository<TModel, T1, T2>
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
    }
}