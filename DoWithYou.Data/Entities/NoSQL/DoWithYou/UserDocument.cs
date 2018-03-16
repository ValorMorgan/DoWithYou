using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using MongoDB.Bson.Serialization.Attributes;

namespace DoWithYou.Data.Entities.NoSQL.DoWithYou
{
    public class UserDocument : IUserDocument
    {
        #region PROPERTIES
        public IAddress Address { get; set; }

        public string Email { get; set; }

        [BsonId]
        public long ID { get; set; }

        public IName Name { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
        #endregion

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
            hashCode = hashCode * HashConstants.MULTIPLIER + ID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Email);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Username);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Password);
            return hashCode;
        }
    }

    public class Address : IAddress
    {
        #region PROPERTIES
        public string City { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
        #endregion
    }

    public class Name : IName
    {
        #region PROPERTIES
        public string First { get; set; }

        public string Last { get; set; }

        public string Middle { get; set; }
        #endregion
    }
}