using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Shared;
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
        protected readonly ILoggerTemplates templates;
        #endregion

        #region CONSTRUCTORS
        protected Repository(IDoWithYouContext context, ILoggerTemplates templates)
        {
            this.templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, this.templates.Constructor, nameof(Repository<T>));

            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, templates.DataDelete, typeof(T).Name);

            _entities.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, templates.DataGetAll, typeof(T).Name);
            return _entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, templates.DataInsert, typeof(T).Name);

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, templates.DataSaveChanges, typeof(T).Name);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, templates.DataUpdate, typeof(T).Name);

            _entities.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, templates.Dispose, $"{nameof(Repository<T>)}<{typeof(T).Name}>");

            _entities = null;

            // NOTE: Purposefully do not dispose of Context. Autofac handles disposing of this object.
        }
    }
}