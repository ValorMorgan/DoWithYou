using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using MongoDB.Bson.Serialization.Attributes;

namespace DoWithYou.Data.Entities.DoWithYou
{
    public class User : BaseEntity, IUser
    {
        [BsonId]
        public long UserID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual UserProfile UserProfile { get; set; }

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