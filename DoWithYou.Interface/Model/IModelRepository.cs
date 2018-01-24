﻿using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
{
    public interface IModelRepository<TModel, T1> : IDisposable
        where TModel : IModel<T1>
        where T1 : IBaseEntity
    {
        void Delete(TModel model);

        TModel Get(T1 entity);

        TModel Get(Func<IQueryable<T1>, T1> request1);

        IEnumerable<TModel> GetMany(IEnumerable<T1> entities);

        IEnumerable<TModel> GetMany(Func<IQueryable<T1>, IEnumerable<T1>> request1);

        void Insert(TModel model);

        void SaveChanges();

        void Update(TModel model);
    }

    public interface IModelRepository<TModel, T1, T2> : IDisposable
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
        void Delete(TModel model);

        TModel Get((T1, T2) entity);

        TModel Get(T1 entity1, T2 entity2);

        TModel Get(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2);

        IEnumerable<TModel> GetMany(IEnumerable<(T1, T2)> entities);

        IEnumerable<TModel> GetMany(IEnumerable<T1> entities1, IEnumerable<T2> entities2);

        IEnumerable<TModel> GetMany(Func<IQueryable<T1>, IEnumerable<T1>> request1, Func<IQueryable<T2>, IEnumerable<T2>> request2);

        void Insert(TModel model);

        void SaveChanges();

        void Update(TModel model);
    }
}