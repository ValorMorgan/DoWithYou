using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Serilog;

namespace DoWithYou.Model.Mappers
{
    public class UserModelMapper : IModelMapper<IUserModel, IUser, IUserProfile>
    {
        #region CONSTRUCTORS
        public UserModelMapper()
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(UserModelMapper));
        }
        #endregion

        public IUserModel MapEntityToModel((IUser, IUserProfile) entity) =>
            MapEntityToModel(entity.Item1, entity.Item2);

        public IUserModel MapEntityToModel(IUser user, IUserProfile profile)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_ENTITY_TO_MODEL_2, nameof(IUser), nameof(IUserProfile), nameof(IUserModel));

            return new UserModel(user, profile);
        }

        public (IUser, IUserProfile) MapModelToEntity(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, LoggerTemplates.MAP_MODEL_TO_ENTITY_2, nameof(IUserModel), nameof(IUser), nameof(IUserProfile));

            IUser user = GetNewUser(model as UserModel);
            IUserProfile profile = GetNewUserProfile(model as UserModel);

            return (user, profile);
        }

        #region PRIVATE
        private IUser GetNewUser(UserModel model) => model == null ?
            new User() :
            new User
            {
                Email = model.Email,
                Password = model.Password,
                UserID = model.UserID ?? default,
                Username = model.Username
            };

        private IUserProfile GetNewUserProfile(UserModel model) => model == null ?
            new UserProfile() :
            new UserProfile
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Phone = model.Phone,
                State = model.State,
                UserID = model.UserID ?? default,
                UserProfileID = model.UserProfileID ?? default,
                ZipCode = model.ZipCode
            };
        #endregion
    }
}