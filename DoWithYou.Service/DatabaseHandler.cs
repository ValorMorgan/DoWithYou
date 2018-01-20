using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;
using DoWithYou.Interface.Shared;
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
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public DatabaseHandler(IRepository<T> repository, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, nameof(DatabaseHandler<T>));

            _repository = repository;
        }
        #endregion

        public void Delete(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestDelete, typeof(T).Name);
            _repository.Delete(entity);
        }

        public T Get(Func<IQueryable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestGetDynamic, typeof(T).Name);

            if (operation == default)
                return default;

            return operation(_repository.GetQueryable());
        }

        public IList<T> GetMany(Func<IQueryable<T>, IQueryable<T>> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestGetDynamic, typeof(T).Name);

            if (operation == default)
                return new List<T>();

            return operation(_repository.GetQueryable())
                ?.ToList() ?? new List<T>();
        }

        public IList<T> GetAll()
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestGetDynamic, typeof(T).Name);
            return _repository.GetQueryable()
                ?.ToList() ?? new List<T>();
        }

        public void Insert(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestInsert, typeof(T).Name);
            _repository.Insert(entity);
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestSaveChanges, typeof(T).Name);
            _repository.SaveChanges();
        }

        public void Update(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestUpdate, typeof(T).Name);
            _repository.Update(entity);
        }

        public void Update(Func<IQueryable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, _templates.RequestUpdateDynamic, typeof(T).Name);
            
            _repository.Update(Get(operation));
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, _templates.Dispose, $"{nameof(DatabaseHandler<T>)}<{typeof(T).Name}>");

            _repository?.Dispose();
            _repository = null;
        }
    }
}