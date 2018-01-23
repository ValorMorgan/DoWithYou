using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Interface.Service
{
    public interface IModelMapper<TModel, T1>
        where TModel : IModel<T1>
        where T1 : IBaseEntity
    {
        TModel MapEntityToModel(T1 entity);

        T1 MapModelToEntity(TModel model);
    }

    public interface IModelMapper<TModel, T1, T2>
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
        TModel MapEntityToModel(T1 entity1, T2 entity2);

        (T1, T2) MapModelToEntity(TModel model);
    }
}