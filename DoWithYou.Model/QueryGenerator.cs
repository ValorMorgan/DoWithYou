using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Model.Repository;

namespace DoWithYou.Model
{
    public class QueryGenerator<T> : IDisposable
        where T : IBaseEntity
    {
        #region VARIABLES
        private IRepository<T> _repository;
        #endregion

        #region CONSTRUCTORS
        public QueryGenerator()
        {
            _repository = GetRepository();
        }

        internal QueryGenerator(IRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        public void Delete(T entity) =>
            _repository.Delete(entity);

        public T Get(long id) =>
            _repository.Get(id);

        public T Get(Func<IEnumerable<T>, T> operation) =>
            operation == default ?
                default :
                operation(_repository.GetAll()?.Select(e => e));

        public void Insert(T entity) =>
            _repository.Insert(entity);

        public void SaveChanges() =>
            _repository.SaveChanges();

        public void Update(T entity) =>
            _repository.Update(entity);

        public void Update(Func<IEnumerable<T>, T> operation)
        {
            if (operation == default)
                return;

            _repository.Update(Get(operation));
        }

        public void Dispose()
        {
            _repository?.Dispose();
            _repository = null;
        }

        #region PRIVATE
        internal IRepository<T> GetRepository()
        {
            switch (typeof(T))
            {
                case Type _ when typeof(T) == typeof(IUser):
                    return new UserRepository() as IRepository<T>;

                case Type _ when typeof(T) == typeof(IUserProfile):
                    return new UserProfileRepository() as IRepository<T>;
            }

            throw new TypeLoadException($"Failed to locate related {nameof(IRepository<T>)} to provided type \"{typeof(T).FullName}\"");
        }
        #endregion
    }
}