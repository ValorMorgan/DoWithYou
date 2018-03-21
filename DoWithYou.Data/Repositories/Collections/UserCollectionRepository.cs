using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Data.Mappers;
using DoWithYou.Data.Repositories.Collections.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Serilog;

namespace DoWithYou.Data.Repositories.Collections
{
    public class UserCollectionRepository : CollectionRepository<UserDocument>, IRepository<IUserDocument>
    {
        #region CONSTRUCTORS
        public UserCollectionRepository(ICollectionDatabaseMapper<IUserDocument> mapper)
            : base(mapper.MapCollectionToContext())
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(UserCollectionRepository));
        }
        #endregion

        public void Delete(IUserDocument document) =>
            base.Delete(document as UserDocument);

        public IUserDocument Get(Func<IQueryable<IUserDocument>, IUserDocument> operation) =>
            base.Get(e => operation(e) as UserDocument);

        public IEnumerable<IUserDocument> GetMany(Func<IQueryable<IUserDocument>, IEnumerable<IUserDocument>> operation) =>
            base.GetMany(e => operation(e).Cast<UserDocument>());

        public void Insert(IUserDocument document) =>
            base.Insert(document as UserDocument);

        public new void SaveChanges() =>
            base.SaveChanges();
        
        public void Update(IUserDocument document) =>
            base.Update(document as UserDocument);

        public new void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, nameof(UserCollectionRepository));

            base.Dispose();
        }
    }
}