using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Mappers;
using DoWithYou.Data.Repositories.Collections.Base;
using DoWithYou.Interface.Model;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Serilog;

namespace DoWithYou.Data.Repositories.Collections
{
    public class UserCollectionRepository : CollectionRepository<IUserModel>
    {
        #region VARIABLES
        private readonly MongoDbContext _context;
        #endregion

        #region CONSTRUCTORS
        public UserCollectionRepository(ICollectionDatabaseMapper<IUserModel> mapper)
            : base(mapper.MapCollectionToContext())
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(UserCollectionRepository));
        }
        #endregion

        public void Delete(IUserModel document) =>
            base.Delete(document);

        public IUserModel Get(Func<IQueryable<IUserModel>, IUserModel> query) =>
            base.Get(query);

        public IEnumerable<IUserModel> GetMany(Func<IQueryable<IUserModel>, IEnumerable<IUserModel>> query) =>
            base.GetMany(query);

        public void Insert(IUserModel document) =>
            base.Insert(document);

        public void Update(IUserModel document) =>
            base.Update(document);

        #region PRIVATE
        private object UserDocument(IUserModel model) => new
        {
            ID = model?.UserID ?? default,
            Username = model?.Username ?? string.Empty,
            Email = model?.Email ?? string.Empty,
            Password = model?.Password ?? string.Empty,
            Address = Address(model),
            Name = Name(model)
        };

        private object Address(IUserModel model) => new
        {
            Line1 = model?.Address1 ?? string.Empty,
            Line2 = model?.Address2 ?? string.Empty,
            City = model?.City ?? string.Empty,
            State = model?.State ?? string.Empty,
            ZipCode = model?.ZipCode ?? string.Empty
        };

        private object Name(IUserModel model) => new
        {
            First = model?.FirstName ?? string.Empty,
            Middle = model?.MiddleName ?? string.Empty,
            Last = model?.LastName ?? string.Empty
        };
        #endregion
    }
}