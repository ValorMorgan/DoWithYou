using System;
using System.Linq;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Interface.Service
{
    public interface IModelRequestor<out TModel, T1>
        where TModel : IModel<T1>
        where T1 : IBaseEntity
    {
        TModel GetModel(Func<IQueryable<T1>, T1> request1);

        TModel GetModel(T1 entity1);
    }

    public interface IModelRequestor<out TModel, T1, T2>
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
        TModel GetModel(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2);

        TModel GetModel(T1 entity1, T2 entity2);
    }
}