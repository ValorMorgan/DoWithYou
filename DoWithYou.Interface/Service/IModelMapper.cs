using System;
using System.Linq;

namespace DoWithYou.Interface.Service
{
    public interface IModelMapper<TModel, T1> : IDisposable
    {
        TModel MapEntityToModel(T1 entity);
        TModel RequestModel(Func<IQueryable<T1>, T1> request);

        T1 MapModelToEntity(TModel model);
    }

    public interface IModelMapper<TModel, T1, T2> : IDisposable
    {
        TModel MapEntityToModel(T1 entity);
        TModel MapEntityToModel(T2 entity);
        TModel MapEntityToModel(T1 entity1, T2 entity2);
        TModel RequestModel(Func<IQueryable<T1>, T1> request);
        TModel RequestModel(Func<IQueryable<T2>, T2> request);
        TModel RequestModel(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2);

        (T1, T2) MapModelToEntity(TModel model);
    }
}
