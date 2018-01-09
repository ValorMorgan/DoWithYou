using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Data;
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
        #endregion

        #region CONSTRUCTORS
        public Repository(IDoWithYouContext context)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(Repository<T>));

            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Deleting {Type}[{EntityId}]", typeof(T).Name, entity.ID);

            _entities.Remove(entity);
            SaveChanges();
        }

        public T Get(long id)
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Getting {Type}[{EntityId}]", typeof(T).Name, id);
            return _entities.SingleOrDefault(e => e.ID == id);
        }

        public IEnumerable<T> GetAll()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Getting all {Type}", typeof(T).Name);
            return _entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Inserting {Type}[{EntityId}]", typeof(T).Name, entity.ID);

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Saving Changes for {Type}", typeof(T).Name);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == default(T))
                return;

            Log.Logger.LogEventInformation(LoggerEvents.DATA, "Updating {Type}[{EntityId}]", typeof(T).Name, entity.ID);

            _entities.Update(entity);
            SaveChanges();
        }

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, "Disposing {Class}", nameof(Repository<T>));

            _entities = null;

            _context?.Dispose();
            _context = null;
        }
    }
}