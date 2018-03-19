using DoWithYou.Data.Entities.Base;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;

namespace DoWithYou.Data.Entities.SQL.DoWithYou
{
    public class User : BaseEntity, IUser
    {
        public long UserID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public override string ToString() =>
$@"{{
    {nameof(UserID)}: {UserID},
    {nameof(Email)}: {Email},
    {nameof(Username)}: {Username},
    {nameof(Password)}: {Password},
    {base.ToString()}
}}";

        public override bool Equals(object obj)
        {
            if (!(obj is IUser))
                return false;

            return GetHashCode() == ((User)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = -1166176521;
            hashCode = hashCode * HashConstants.MULTIPLIER + base.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + UserID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Email);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Username);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Password);
            return hashCode;
        }
    }
}