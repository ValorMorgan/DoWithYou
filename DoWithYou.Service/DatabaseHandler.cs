using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;

namespace DoWithYou.Service
{
    public class DatabaseHandler<T> : IDatabaseHandler<T>
        where T : IBaseEntity
    {
        #region VARIABLES
        private IRepository<T> _repository;
        #endregion

        #region CONSTRUCTORS
        public DatabaseHandler(IRepository<T> repository)
        {
            //Resolver.Resolve<Serilog.ILogger>().Verbose("{Class} constructor entered.", nameof(QueryGenerator<T>));
            // or
            //Log.Verbose("{Class} constructor entered.", nameof(QueryGenerator<T>));
            // or
            //var logger = Resolver.Resolve<ILogger>();
            //logger.Verbose("{Class} constructor entered.", nameof(QueryGenerator<T>));
            // or
            // ... use ILogger from constructor, DI ...
            // public QueryGenerator(ILogger logger, ...)
            // public QueryGenerator(ILogger logger, IApplicationSettings settings, ...)
            // At UI, do "Resolver.Resolve(<Service Layer Class>)" would chain down to Data Layer doing DI

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
    }
}