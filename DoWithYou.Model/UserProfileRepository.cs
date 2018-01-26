using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Model.Base;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Model
{
    public class UserProfileRepository : EntityRepository<UserProfile>, IRepository<IUserProfile>
    {
        #region CONSTRUCTORS
        public UserProfileRepository(IDoWithYouContext context)
            : base(context) { }
        
        internal UserProfileRepository(IDoWithYouContext context, DbSet<UserProfile> entities)
            : base(context, entities) { }
        #endregion

        public void Delete(IUserProfile entity) =>
            base.Delete(entity as UserProfile);

        public IUserProfile Get(Func<IQueryable<IUserProfile>, IUserProfile> operation) =>
            base.Get(e => operation(e) as UserProfile);

        public IEnumerable<IUserProfile> GetMany(Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>> operation) =>
            base.GetMany(e => operation(e).Cast<UserProfile>());

        public void Insert(IUserProfile entity) =>
            base.Insert(entity as UserProfile);

        public void Update(IUserProfile entity) =>
            base.Update(entity as UserProfile);

        public new void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, nameof(UserProfileRepository));

            base.Dispose();
        }
    }
}