using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Model.Base
{
    public abstract class EntityRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region VARIABLES
        private readonly DbContext _context;
        private DbSet<T> _entities;
        #endregion

        #region CONSTRUCTORS
        protected EntityRepository(DbContext context)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(EntityRepository<T>));

            _context = context;
            _entities = _context.Set<T>();
        }

        internal EntityRepository(DbContext context, DbSet<T> entiities)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(EntityRepository<T>));

            _context = context;
            _entities = entiities;
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DATA_DELETE, typeof(T).Name);

            _entities.Remove(entity);
            SaveChanges();
        }

        public T Get(Func<IQueryable<T>, T> operation) => operation(GetQueryable());

        public IEnumerable<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> operation) => operation(GetQueryable());

        public IQueryable<T> GetQueryable()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DATA_GET_ALL, typeof(T).Name);
            return _entities.AsQueryable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DATA_INSERT, typeof(T).Name);

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DATA_SAVE_CHANGES, typeof(T).Name);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, LoggerTemplates.DATA_UPDATE, typeof(T).Name);

            _entities.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, $"{nameof(EntityRepository<T>)}<{typeof(T).Name}>");

            _entities = null;

            // NOTE: Purposefully do not dispose of Context. Autofac handles disposing of this object.
        }
    }
}