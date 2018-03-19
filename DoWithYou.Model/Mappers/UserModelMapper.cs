using System;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Data.Entities.SQL.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Serilog;

namespace DoWithYou.Model.Mappers
{
    public class UserModelMapper : IModelMapper<IUserModel, IUserDocument>, IModelMapper<IUserModel, IUser, IUserProfile>
    {
        #region CONSTRUCTORS
        public UserModelMapper()
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(UserModelMapper));
        }
        #endregion

        public IUserModel MapDocumentToModel(IUserDocument document)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_ENTITY_TO_MODEL_1, nameof(IUserDocument), nameof(IUserModel));

            return document == null ?
                new UserModel() :
                new UserModel
                {
                    Address = document.Address,
                    CreationDate = document.CreationDate,
                    Email = document.Email,
                    ModifiedDate = document.ModifiedDate,
                    Name = document.Name,
                    Password = document.Password,
                    Phone = document.Phone,
                    UserID = document.ID,
                    UserProfileID = default, // NOTE: Document never holds any more than 1 ID
                    Username = document.Username
                };
        }

        public IUserModel MapEntityToModel((IUser, IUserProfile) entity) =>
            MapEntityToModel(entity.Item1, entity.Item2);

        public IUserModel MapEntityToModel(IUser entity1, IUserProfile entity2)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_ENTITY_TO_MODEL_2, nameof(IUser), nameof(IUserProfile), nameof(IUserModel));

            return entity1 == null && entity2 == null ?
                new UserModel() :
                new UserModel
            {
                Address = new Address
                {
                    City = entity2?.City,
                    Line1 = entity2?.Address1,
                    Line2 = entity2?.Address2,
                    State = entity2?.State,
                    ZipCode = entity2?.ZipCode
                },
                CreationDate = entity1?.CreationDate ?? entity2?.CreationDate,
                Email = entity1?.Email,
                UserID = entity1?.UserID,
                ModifiedDate = entity1?.ModifiedDate ?? entity2?.ModifiedDate,
                Name = new Name
                {
                    First = entity2?.FirstName,
                    Middle = entity2?.MiddleName,
                    Last = entity2?.LastName
                },
                Password = entity1?.Password,
                Phone = entity2?.Phone,
                Username = entity1?.Username
            };
        }

        public IUserDocument MapModelToDocument(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_MODEL_TO_ENTITY_1, nameof(IUserModel), nameof(IUserDocument));

            return GetNewUserDocument(model as UserModel);
        }

        public (IUser, IUserProfile) MapModelToEntity(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_MODEL_TO_ENTITY_2, nameof(IUserModel), nameof(IUser), nameof(IUserProfile));

            IUser user = GetNewUser(model as UserModel);
            IUserProfile profile = GetNewUserProfile(model as UserModel);

            return (user, profile);
        }

        #region PRIVATE
        private static IUser GetNewUser(UserModel model) => model == null ?
            new User() :
            new User
            {
                Email = model.Email,
                Password = model.Password,
                UserID = model.UserID ?? default,
                Username = model.Username,
                CreationDate = model.CreationDate ?? DateTime.Now,
                ModifiedDate = model.ModifiedDate
            };

        private static IUserDocument GetNewUserDocument(UserModel model) => model == null ?
            new UserDocument() :
            new UserDocument
            {
                ID = model.UserID ?? default,
                Address = model.Address,
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
                Phone = model.Phone,
                Username = model.Username,
                CreationDate = model.CreationDate ?? DateTime.Now,
                ModifiedDate = model.ModifiedDate
            };

        private static IUserProfile GetNewUserProfile(UserModel model) => model == null ?
            new UserProfile() :
            new UserProfile
            {
                Address1 = model.Address.Line1,
                Address2 = model.Address.Line2,
                City = model.Address.City,
                FirstName = model.Name.First,
                LastName = model.Name.Last,
                MiddleName = model.Name.Middle,
                Phone = model.Phone,
                State = model.Address.State,
                UserID = model.UserID ?? default,
                UserProfileID = model.UserProfileID ?? default,
                ZipCode = model.Address.ZipCode,
                CreationDate = model.CreationDate ?? DateTime.Now,
                ModifiedDate = model.ModifiedDate
            };
        #endregion
    }
}