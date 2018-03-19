using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using MongoDB.Driver;
using Serilog;

namespace DoWithYou.Data.Repositories.Collections.Base
{
    public class CollectionRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region VARIABLES
        private IMongoCollection<T> _collection;
        private readonly MongoDbContext _context;
        #endregion

        #region CONSTRUCTORS
        public CollectionRepository(MongoDbContext context)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(CollectionRepository<T>));

            _context = context;
            _collection = _context.GetCollection<T>();
        }

        internal CollectionRepository(MongoDbContext context, IMongoCollection<T> collection)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(CollectionRepository<T>));

            _context = context;
            _collection = collection;
        }
        #endregion

        public void Delete(T document) =>
            throw new NotImplementedException();

        public T Get(Func<IQueryable<T>, T> query) =>
            throw new NotImplementedException();

        public IEnumerable<T> GetMany(Func<IQueryable<T>, IEnumerable<T>> query) =>
            throw new NotImplementedException();

        public void Insert(T document) =>
            throw new NotImplementedException();

        public void SaveChanges() =>
            throw new InvalidOperationException($"NoSQL should not use an explicit {nameof(SaveChanges)}() operation.");

        public void Update(T document) =>
            throw new NotImplementedException();

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, $"{nameof(CollectionRepository<T>)}<{typeof(T).Name}>");

            _collection = null;

            // NOTE: Purposefully do not dispose of Context. Autofac handles disposing of this object.
        }
    }
}