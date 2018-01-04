using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface;

namespace DoWithYou.Data.Entities.DoWithYou
{
    public class UserProfile : BaseEntity, IUserProfile
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Phone { get; set; }

        public string State { get; set; }

        public virtual IUser User { get; set; }

        public string ZipCode { get; set; }
    }
}