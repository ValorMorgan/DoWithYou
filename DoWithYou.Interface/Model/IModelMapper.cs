using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
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
        TModel MapEntityToModel((T1, T2) entity);

        TModel MapEntityToModel(T1 entity1, T2 entity2);

        (T1, T2) MapModelToEntity(TModel model);
    }
}