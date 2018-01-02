using System.Collections.Generic;

namespace DoWithYou.Interface
{
    public interface IRepository<T>
    {
        void Delete(T entity);
        T Get(long id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void SaveChanges();
        void Update(T entity);
    }
}