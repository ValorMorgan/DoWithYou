using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
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
            Log.Logger.LogEventInformation(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(DatabaseHandler<T>));

            _repository = repository;
        }
        #endregion

        public void Delete(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Delete {Type}[{EntityId}]", typeof(T).Name, entity?.ID ?? -1);
            _repository.Delete(entity);
        }

        public T Get(long id)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Get {Type}[{EntityId}]", typeof(T).Name, id);
            return _repository.Get(id);
        }

        public T Get(Func<IEnumerable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Get {Type} via dynamic request", typeof(T).Name);
            return operation == default ?
                default :
                operation(_repository.GetAll()?.Select(e => e));
        }

        public void Insert(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Insert {Type}[{EntityId}]", typeof(T).Name, entity?.ID ?? -1);
            _repository.Insert(entity);
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to SaveChanges for {Type}", typeof(T).Name);
            _repository.SaveChanges();
        }

        public void Update(T entity)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Update {Type}[{EntityId}]", typeof(T).Name, entity?.ID ?? -1);
            _repository.Update(entity);
        }

        public void Update(Func<IEnumerable<T>, T> operation)
        {
            Log.Logger.LogEventInformation(LoggerEvents.REQUEST, "Requested to Insert {Type} via dynamic request", typeof(T).Name);

            if (operation == default)
                return;

            _repository.Update(Get(operation));
        }

        public void Dispose()
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DISPOSE, "Disposing {Class}", nameof(DatabaseHandler<T>));

            _repository?.Dispose();
            _repository = null;
        }
    }
}