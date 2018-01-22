using System;
using System.Linq;

namespace DoWithYou.Interface.Data
{
    public interface IRepository<T> : IDisposable
    {
        void Delete(T entity);

        IQueryable<T> GetQueryable();

        void Insert(T entity);

        void SaveChanges();

        void Update(T entity);
    }
}