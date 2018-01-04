using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface;

namespace DoWithYou.Data.Entities.DoWithYou
{
    public class User : BaseEntity, IUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public virtual IUserProfile UserProfile { get; set; }
    }
}