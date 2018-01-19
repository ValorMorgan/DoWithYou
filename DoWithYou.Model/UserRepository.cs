using System;
using System.Collections.Generic;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Shared;
using DoWithYou.Model.Base;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Model
{
    public class UserRepository : Repository<User>, IRepository<IUser>
    {
        #region CONSTRUCTORS
        public UserRepository(IDoWithYouContext context, ILoggerTemplates templates)
            : base(context, templates) { }
        #endregion

        public void Delete(IUser entity) =>
            base.Delete(entity as User);

        public IUser Get(Func<IUser, bool> operation) =>
            base.Get(operation);

        public IEnumerable<IUser> GetMany(Func<IUser, bool> operation) =>
            base.GetMany(operation);

        public void Insert(IUser entity) =>
            base.Insert(entity as User);

        public void Update(IUser entity) =>
            base.Update(entity as User);

        public new void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, templates.Dispose, nameof(UserRepository));
            
            base.Dispose();
        }

        #region PRIVATE
        IEnumerable<IUser> IRepository<IUser>.GetAll() =>
            base.GetAll();
        #endregion
    }
}