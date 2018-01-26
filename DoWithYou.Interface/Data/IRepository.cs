using System;
using System.Collections.Generic;
using System.Linq;

namespace DoWithYou.Interface.Data
{
    public interface IRepository<T> : IDisposable
    {
        void Delete(T entity);

        T Get(Func<IQueryable<T>, T> operation);

        IEnumerable<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> operation);

        void Insert(T entity);

        void SaveChanges();

        void Update(T entity);
    }
}