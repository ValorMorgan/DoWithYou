using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Service;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

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
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.Constructor, nameof(DatabaseHandler<T>));

            _repository = repository;
        }
        #endregion

        public void Delete(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestDelete, typeof(T).Name);
            _repository.Delete(entity);
        }

        public T Get(Func<IQueryable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestGetDynamic, typeof(T).Name);

            if (operation == default)
                return default;

            return _repository.Get(operation);
        }

        public IList<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestGetDynamic, typeof(T).Name);

            if (operation == default)
                return new List<T>();

            return _repository.GetMany(operation)
                ?.ToList() ?? new List<T>();
        }

        public IList<T> GetAll()
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestGetDynamic, typeof(T).Name);
            return _repository.GetMany(e => e)
                ?.ToList() ?? new List<T>();
        }

        public void Insert(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestInsert, typeof(T).Name);
            _repository.Insert(entity);
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestSaveChanges, typeof(T).Name);
            _repository.SaveChanges();
        }

        public void Update(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestUpdate, typeof(T).Name);
            _repository.Update(entity);
        }

        public void Update(Func<IQueryable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, LoggerTemplates.RequestUpdateDynamic, typeof(T).Name);
            
            _repository.Update(Get(operation));
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.Disposing, $"{nameof(DatabaseHandler<T>)}<{typeof(T).Name}>");

            _repository?.Dispose();
            _repository = null;
        }
    }
}