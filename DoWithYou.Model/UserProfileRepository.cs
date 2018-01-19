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
    public class UserProfileRepository : Repository<UserProfile>, IRepository<IUserProfile>
    {
        #region CONSTRUCTORS
        public UserProfileRepository(IDoWithYouContext context, ILoggerTemplates templates)
            : base(context, templates) { }
        #endregion

        public void Delete(IUserProfile entity) =>
            base.Delete(entity as UserProfile);

        public IUserProfile Get(Func<IUserProfile, bool> operation) =>
            base.Get(operation);

        public IEnumerable<IUserProfile> GetMany(Func<IUserProfile, bool> operation) =>
            base.GetMany(operation);

        public void Insert(IUserProfile entity) =>
            base.Insert(entity as UserProfile);

        public void Update(IUserProfile entity) =>
            base.Update(entity as UserProfile);

        public new void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, templates.Dispose, nameof(UserProfileRepository));

            base.Dispose();
        }

        #region PRIVATE
        IEnumerable<IUserProfile> IRepository<IUserProfile>.GetAll() =>
            base.GetAll();
        #endregion
    }
}