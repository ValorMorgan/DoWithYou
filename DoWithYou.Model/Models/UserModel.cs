using System.ComponentModel.DataAnnotations;
using DoWithYou.Interface.Model;

namespace DoWithYou.Model.Models
{
    public class UserModel : IUserModel
    {
        #region VARIABLES
        private string _address1;
        private string _address2;
        private string _city;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _phone;
        private string _state;
        private string _username;
        private string _zipCode;
        #endregion

        #region PROPERTIES
        public string Address1
        {
            get => _address1;
            set => _address1 = value?.Trim() ?? string.Empty;
        }

        public string Address2
        {
            get => _address2;
            set => _address2 = value?.Trim() ?? string.Empty;
        }

        public string City
        {
            get => _city;
            set => _city = value?.Trim() ?? string.Empty;
        }

        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim() ?? string.Empty;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value?.Trim() ?? string.Empty;
        }

        public string FullAddress => $"{$"{Address1} {Address2}".Trim()}, {City}, {State}, {ZipCode}".Trim();

        public string FullName => $"{$"{FirstName} {MiddleName}".Trim()} {LastName}".Trim();

        public string LastName
        {
            get => _lastName;
            set => _lastName = value?.Trim() ?? string.Empty;
        }

        public string MiddleName
        {
            get => _middleName;
            set => _middleName = value?.Trim() ?? string.Empty;
        }

        [Phone]
        public string Phone
        {
            get => _phone;
            set => _phone = value?.Trim() ?? string.Empty;
        }

        public string State
        {
            get => _state;
            set => _state = value?.Trim() ?? string.Empty;
        }

        public string Username
        {
            get => _username;
            set => _username = value?.Trim() ?? string.Empty;
        }

        public string ZipCode
        {
            get => _zipCode;
            set => _zipCode = value?.Trim() ?? string.Empty;
        }

        internal long UserID { get; set; }
        #endregion
    }
}