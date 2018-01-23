using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Model.Base
{
    public abstract class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region VARIABLES
        private readonly IDoWithYouContext _context;
        private DbSet<T> _entities;
        #endregion

        #region CONSTRUCTORS
        protected Repository(IDoWithYouContext context)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.Constructor, nameof(Repository<T>));

            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        public T Get(Func<IQueryable<T>, T> operation)
        {
            return operation(GetQueryable());
        }

        public IEnumerable<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> operation)
        {
            return operation(GetQueryable());
        }

        public void Delete(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DataDelete, typeof(T).Name);

            _entities.Remove(entity);
            SaveChanges();
        }

        public IQueryable<T> GetQueryable()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DataGetAll, typeof(T).Name);
            return _entities.AsQueryable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DataInsert, typeof(T).Name);

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DataSaveChanges, typeof(T).Name);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DataUpdate, typeof(T).Name);

            _entities.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.Disposing, $"{nameof(Repository<T>)}<{typeof(T).Name}>");

            _entities = null;

            // NOTE: Purposefully do not dispose of Context. Autofac handles disposing of this object.
        }
    }
}