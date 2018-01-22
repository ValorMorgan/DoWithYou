using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Service
{
    public interface IDatabaseHandler<T> : IDisposable
        where T : IBaseEntity
    {
        void Delete(T entity);

        T Get(Func<IQueryable<T>, T> operation);

        IList<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> operation);

        IList<T> GetAll();

        void Insert(T entity);

        void SaveChanges();

        void Update(Func<IQueryable<T>, T> operation);

        void Update(T entity);
    }
}