using System;
using System.Collections.Generic;

namespace DoWithYou.Interface.Data
{
    public interface IRepository<T> : IDisposable
    {
        void Delete(T entity);

        T Get(long id);

        IEnumerable<T> GetAll();

        void Insert(T entity);

        void SaveChanges();

        void Update(T entity);
    }
}