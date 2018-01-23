using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using DoWithYou.Model.Models;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Model.Mappers
{
    public class UserModelMapper : IModelMapper<IUserModel, IUser, IUserProfile>
    {
        #region CONSTRUCTORS
        public UserModelMapper()
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(UserModelMapper));
        }
        #endregion

        public IUserModel MapEntityToModel(IUser user, IUserProfile profile)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom1} & {ClassFrom2} to {ClassTo}", nameof(IUser), nameof(IUserProfile), nameof(IUserModel));

            return new UserModel(user, profile);
        }

        public (IUser, IUserProfile) MapModelToEntity(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo1} & {ClassTo2}", nameof(IUserModel), nameof(IUser), nameof(IUserProfile));

            IUser user = GetNewUser((UserModel)model);
            IUserProfile profile = GetNewUserProfile((UserModel)model);

            return (user, profile);
        }

        #region PRIVATE
        private IUser GetNewUser(UserModel model) => new User
        {
            Email = model.Email,
            Password = model.Password,
            UserID = model.UserID,
            Username = model.Username
        };

        private IUserProfile GetNewUserProfile(UserModel model) => new UserProfile
        {
            Address1 = model.Address1,
            Address2 = model.Address2,
            City = model.City,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            Phone = model.Phone,
            State = model.State,
            UserID = model.UserID,
            UserProfileID = model.UserProfileID,
            ZipCode = model.ZipCode
        };
        #endregion
    }
}