using System;
using System.Linq;

namespace DoWithYou.Interface.Service
{
    public interface IModelRequestor<T1, out TModel> : IDisposable {
        TModel GetModelFromEntity(T1 entity);
        TModel RequestModel(Func<IQueryable<T1>, T1> request);
    }

    public interface IModelRequestor<T1, T2, out TModel> : IDisposable
    {
        TModel GetModelFromEntity(T1 entity);
        TModel GetModelFromEntity(T2 entity);
        TModel GetModelFromEntity(T1 entity1, T2 entity2);

        TModel RequestModel(Func<IQueryable<T1>, T1> request);
        TModel RequestModel(Func<IQueryable<T2>, T2> request);
        TModel RequestModel(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2);
    }
}