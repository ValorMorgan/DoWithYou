using DoWithYou.Data.Entities.DoWithYou.Base;

namespace DoWithYou.Data.Entities.DoWithYou
{
    public class User : BaseEntity
    {
        #region PROPERTIES
        public string Email { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
        
        public virtual UserProfile UserProfile { get; set; }
        #endregion
    }
}