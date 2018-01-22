using System;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
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
        #region VARIABLES
        private IRepository<IUserProfile> _userProfileRepository;
        private IRepository<IUser> _userRepository;
        #endregion

        #region CONSTRUCTORS
        public UserModelMapper(IRepository<IUser> userRepository, IRepository<IUserProfile> userProfileRepository)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(UserModelMapper));

            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        }
        #endregion

        public IUserModel MapEntityToModel(IUser user)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUser), nameof(IUserModel));

            IUserProfile profile = _userProfileRepository.Get(profiles => profiles
                .FirstOrDefault(p => p.UserID == user.UserID));

            return new UserModel(user, profile);
        }

        public IUserModel MapEntityToModel(IUserProfile profile)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo}", nameof(IUserProfile), nameof(IUserModel));

            IUser user = _userRepository.Get(users => users
                .FirstOrDefault(u => u.UserID == profile.UserID));

            return new UserModel(user, profile);
        }

        public IUserModel MapEntityToModel(IUser user, IUserProfile profile)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom1} & {ClassFrom2} to {ClassTo}", nameof(IUser), nameof(IUserProfile), nameof(IUserModel));

            return new UserModel(user, profile);
        }

        public (IUser, IUserProfile) MapModelToEntity(IUserModel model)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Mapping {ClassFrom} to {ClassTo1} & {ClassTo2}", nameof(IUserModel), nameof(IUser), nameof(IUserProfile));

            IUser user = GetUserFromModel(model);
            IUserProfile profile = GetUserProfileFromModel(model);

            if (user == default(IUser))
            {
                InsertNewUserToRepository(model);
                user = GetUserFromModel(model);
            }

            if (profile == default(IUserProfile))
            {
                InsertNewUserProfileToRepository(model, user);
                profile = GetUserProfileFromModel(model);
            }

            return (user, profile);
        }

        public IUserModel RequestModel(Func<IQueryable<IUser>, IUser> request)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Requesting {Model} from dynamic request.", nameof(IUserModel));

            IUser user = _userRepository.Get(request);

            IUserProfile profile = _userProfileRepository.Get(profiles => profiles
                .FirstOrDefault(p => p.UserID == user.UserID));

            return new UserModel(user, profile);
        }

        public IUserModel RequestModel(Func<IQueryable<IUserProfile>, IUserProfile> request)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Requesting {Model} from dynamic request.", nameof(IUserModel));

            IUserProfile profile = _userProfileRepository.Get(request);

            IUser user = _userRepository.Get(users => users
                .FirstOrDefault(u => u.UserID == profile.UserID));

            return new UserModel(user, profile);
        }

        public IUserModel RequestModel(Func<IQueryable<IUser>, IUser> request1, Func<IQueryable<IUserProfile>, IUserProfile> request2)
        {
            Log.Logger.LogEventInformation(LoggerEvents.MAPPING, "Requesting {Model} from dynamic request.", nameof(IUserModel));

            IUser user = _userRepository.Get(request1);
            IUserProfile profile = _userProfileRepository.Get(request2);

            return new UserModel(user, profile);
        }

        public void Dispose()
        {
            _userProfileRepository?.Dispose();
            _userProfileRepository = null;

            _userRepository?.Dispose();
            _userRepository = null;
        }

        #region PRIVATE
        private IUser GetNewUser(IUserModel model) => new User
        {
            Email = model.Email,
            Password = ((UserModel)model).Password,
            Username = model.Username
        };

        private IUserProfile GetNewUserProfile(IUserModel model, IUser user) => new UserProfile
        {
            Address1 = model.Address1,
            Address2 = model.Address2,
            City = model.City,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            Phone = model.Phone,
            State = model.State,
            UserID = user.UserID,
            ZipCode = model.ZipCode
        };

        private IUser GetUserFromModel(IUserModel model)
        {
            return _userRepository.Get(users => users
                .FirstOrDefault(p => p.UserID == ((UserModel)model).UserID));
        }

        private IUserProfile GetUserProfileFromModel(IUserModel model)
        {
            return _userProfileRepository.Get(profiles => profiles
                .FirstOrDefault(p => p.UserID == ((UserModel)model).UserID));
        }

        private void InsertNewUserProfileToRepository(IUserModel model, IUser user)
        {
            _userProfileRepository.Insert(GetNewUserProfile(model, user));
        }

        private void InsertNewUserToRepository(IUserModel model)
        {
            _userRepository.Insert(GetNewUser(model));
        }
        #endregion
    }
}