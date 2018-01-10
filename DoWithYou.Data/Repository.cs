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

namespace DoWithYou.Data
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region VARIABLES
        private IDoWithYouContext _context;
        private DbSet<T> _entities;
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public Repository(IDoWithYouContext context, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, nameof(Repository<T>));

            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataDelete, typeof(T).Name, entity.ID);

            _entities.Remove(entity);
            SaveChanges();
        }

        public T Get(long id)
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataGet, typeof(T).Name, id);
            return _entities.SingleOrDefault(e => e.ID == id);
        }

        public IEnumerable<T> GetAll()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataGetAll, typeof(T).Name);
            return _entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataInsert, typeof(T).Name, entity.ID);

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataSaveChanges, typeof(T).Name);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, _templates.DataUpdate, typeof(T).Name, entity.ID);

            _entities.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, _templates.Dispose, nameof(Repository<T>));

            _entities = null;

            _context?.Dispose();
            _context = null;
        }
    }
}