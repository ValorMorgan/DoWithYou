using System;
using System.Collections.Generic;
using DoWithYou.Interface.Data.Entity;

namespace DoWithYou.Interface.Service
{
    public interface IDatabaseHandler<T> : IDisposable
        where T : IBaseEntity
    {
        void Delete(T entity);
        T Get(Func<IEnumerable<T>, T> operation);
        void Insert(T entity);
        void SaveChanges();
        void Update(Func<IEnumerable<T>, T> operation);
        void Update(T entity);
    }
}