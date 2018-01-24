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
    public class UserRepository : EntityRepository<User>, IRepository<IUser>
    {
        #region CONSTRUCTORS
        public UserRepository(IDoWithYouContext context)
            : base(context) { }

        internal UserRepository(IDoWithYouContext context, DbSet<User> entities)
            : base(context, entities) { }
        #endregion

        public void Delete(IUser entity) =>
            base.Delete(entity as User);

        public IUser Get(Func<IQueryable<IUser>, IUser> operation) =>
            base.Get(e => operation(e) as User);

        public IEnumerable<IUser> GetMany(Func<IQueryable<IUser>, IEnumerable<IUser>> operation) =>
            base.GetMany(e => operation(e).Cast<User>());

        public void Insert(IUser entity) =>
            base.Insert(entity as User);

        public void Update(IUser entity) =>
            base.Update(entity as User);

        public new void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, LoggerTemplates.DISPOSING, nameof(UserRepository));

            base.Dispose();
        }
    }
}