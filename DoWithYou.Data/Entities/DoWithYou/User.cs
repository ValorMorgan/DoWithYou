using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Entity;

namespace DoWithYou.Data.Entities.DoWithYou
{
    public class User : BaseEntity, IUser
    {
        public long UserID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}