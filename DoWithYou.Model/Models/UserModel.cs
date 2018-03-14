using System.ComponentModel.DataAnnotations;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Model.Models
{
    public class UserModel : IUserModel
    {
        #region VARIABLES
        private IUser _user;
        private IUserProfile _userProfile;
        #endregion

        #region PROPERTIES
        public string Address1
        {
            get => UserProfile.Address1?.Trim() ?? string.Empty;
            set => UserProfile.Address1 = value?.Trim();
        }

        public string Address2
        {
            get => UserProfile.Address2?.Trim() ?? string.Empty;
            set => UserProfile.Address2 = value?.Trim();
        }

        public string City
        {
            get => UserProfile.City?.Trim() ?? string.Empty;
            set => UserProfile.City = value?.Trim();
        }

        [EmailAddress]
        public string Email
        {
            get => User.Email?.Trim() ?? string.Empty;
            set => User.Email = value?.Trim();
        }

        public string FirstName
        {
            get => UserProfile.FirstName?.Trim() ?? string.Empty;
            set => UserProfile.FirstName = value?.Trim();
        }

        public string LastName
        {
            get => UserProfile.LastName?.Trim() ?? string.Empty;
            set => UserProfile.LastName = value?.Trim();
        }

        public string MiddleName
        {
            get => UserProfile.MiddleName?.Trim() ?? string.Empty;
            set => UserProfile.MiddleName = value?.Trim();
        }

        public string Password
        {
            get => User.Password?.Trim() ?? string.Empty;
            set => User.Password = value?.Trim();
        }

        [Phone]
        public string Phone
        {
            get => UserProfile.Phone?.Trim() ?? string.Empty;
            set => UserProfile.Phone = value?.Trim();
        }

        public string State
        {
            get => UserProfile.State?.Trim() ?? string.Empty;
            set => UserProfile.State = value?.Trim();
        }

        public string Username
        {
            get => User.Username?.Trim() ?? string.Empty;
            set => User.Username = value?.Trim();
        }

        public string ZipCode
        {
            get => UserProfile.ZipCode?.Trim() ?? string.Empty;
            set => UserProfile.ZipCode = value?.Trim();
        }
        
        public long? UserID
        {
            get => User?.UserID;
            set => User.UserID = value ?? default;
        }

        public long? UserProfileID
        {
            get => UserProfile?.UserProfileID;
            set => UserProfile.UserProfileID = value ?? default;
        }

        private IUser User => _user ?? (_user = new User());

        private IUserProfile UserProfile => _userProfile ?? (_userProfile = new UserProfile());
        #endregion

        #region CONSTRUCTORS
        public UserModel(IUser user, IUserProfile profile)
        {
            _user = user ?? new User();
            _userProfile = profile ?? new UserProfile();
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is IUserModel))
                return false;

            return GetHashCode() == ((UserModel)obj).GetHashCode();
        }

        // Average Hash of included Entities
        public override int GetHashCode() => 
            (User?.GetHashCode() ?? 0 + UserProfile?.GetHashCode() ?? 0) / 2;
    }
}