using System;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using DoWithYou.Shared.Extensions;

namespace DoWithYou.Model.Models
{
    public class UserModel : IUserModel
    {
        #region VARIABLES
        private IAddress _address;

        private DateTime? _creationDate;

        private string _email;

        private DateTime? _modifiedDate;

        private IName _name;

        private string _password;

        private string _phone;

        private long? _userId;

        private string _username;

        private long? _userProfileId;
        #endregion

        #region PROPERTIES
        public IAddress Address
        {
            get => new Address
            {
                Line1 = _address?.Line1?.Trim() ?? string.Empty,
                Line2 = _address?.Line2?.Trim() ?? string.Empty,
                City = _address?.City?.Trim() ?? string.Empty,
                State = _address?.State?.Trim() ?? string.Empty,
                ZipCode = _address?.ZipCode?.Trim() ?? string.Empty
            };
            set => _address = new Address
            {
                Line1 = value?.Line1?.Trim(),
                Line2 = value?.Line2?.Trim(),
                City = value?.City?.Trim(),
                State = value?.State?.Trim(),
                ZipCode = value?.ZipCode?.Trim()
            };
        }

        public DateTime? CreationDate
        {
            get => _creationDate ?? DateTime.Now;
            set => _creationDate = value;
        }

        public string Email
        {
            get => _email?.Trim() ?? string.Empty;
            set => _email = value;
        }

        public DateTime? ModifiedDate
        {
            get => _modifiedDate ?? DateTime.Now;
            set => _modifiedDate = value;
        }

        public IName Name
        {
            get => new Name
            {
                First = _name?.First?.Trim() ?? string.Empty,
                Middle = _name?.Middle?.Trim() ?? string.Empty,
                Last = _name?.Last?.Trim() ?? string.Empty
            };
            set => _name = new Name
            {
                First = value?.First?.Trim(),
                Middle = value?.Middle?.Trim(),
                Last = value?.Last?.Trim()
            };
        }

        public string Password
        {
            get => _password?.Trim() ?? string.Empty;
            set => _password = value;
        }

        public string Phone
        {
            get => _phone?.Trim() ?? string.Empty;
            set => _phone = value;
        }

        public long? UserID
        {
            get => _userId ?? default;
            set => _userId = value;
        }

        public string Username
        {
            get => _username?.Trim() ?? string.Empty;
            set => _username = value;
        }

        public long? UserProfileID
        {
            get => _userProfileId ?? default;
            set => _userProfileId = value;
        }
        #endregion
        
        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IUserModel))
                return false;

            return GetHashCode() == ((UserModel)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = -173635769;
            hashCode = hashCode * HashConstants.MULTIPLIER + UserID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + UserProfileID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + Name.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + Address.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Email);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Username);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Password);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Phone);
            hashCode = hashCode * HashConstants.MULTIPLIER + (CreationDate?.TruncateToSecond().GetHashCode() ?? 0);
            hashCode = hashCode * HashConstants.MULTIPLIER + (ModifiedDate?.TruncateToSecond().GetHashCode() ?? 0);
            return hashCode;
        }
    }
}