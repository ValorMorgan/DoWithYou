using System;
using System.Linq;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using DoWithYou.Model.Models;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Model.Mappers
{
    public class UserModelMapper : IUserModelMapper
    {
        #region VARIABLES
        private readonly IDatabaseHandler<IUserProfile> _profileHandler;
        private readonly IDatabaseHandler<IUser> _userHandler;
        #endregion

        #region CONSTRUCTORS
        public UserModelMapper(IDatabaseHandler<IUser> userHandler, IDatabaseHandler<IUserProfile> profileHandler)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(UserModelMapper));

            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(userHandler));
            _profileHandler = profileHandler ?? throw new ArgumentNullException(nameof(profileHandler));
        }
        #endregion

        public IUserModel GetUserModel(IUser user, IUserProfile profile) =>
            new UserModel
            {
                Address1 = profile.Address1,
                Address2 = profile.Address2,
                City = profile.City,
                Email = user.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
                Phone = profile.Phone,
                State = profile.State,
                UserID = user.UserID,
                Username = user.Username,
                ZipCode = profile.ZipCode
            };

        public IUser MapUserModelToUser(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUserModel), nameof(IUser));

            return _userHandler.Get(users => users.FirstOrDefault(u => u.UserID == ((UserModel)model).UserID));
        }

        public IUserProfile MapUserModelToUserProfile(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUserModel), nameof(IUserProfile));

            return _profileHandler.Get(profiles => profiles.FirstOrDefault(p => p.UserID == ((UserModel)model).UserID));
        }

        public IUserModel MapUserProfileToUserModel(IUserProfile profile)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUserProfile), nameof(IUserModel));

            IUser user = _userHandler.Get(users => users.FirstOrDefault(u => u.UserID == profile.UserID));

            return GetUserModel(user, profile);
        }

        public IUserModel MapUserToUserModel(IUser user)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUser), nameof(IUserModel));

            IUserProfile profile = _profileHandler.Get(profiles => profiles.FirstOrDefault(p => p.UserID == user.UserID));

            return GetUserModel(user, profile);
        }
    }
}