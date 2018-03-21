using DoWithYou.Data.Entities.Base;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using MongoDB.Bson.Serialization.Attributes;

namespace DoWithYou.Data.Entities.NoSQL.DoWithYou
{
    public class UserDocument : BaseEntity, IUserDocument
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

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IUserDocument))
                return false;

            return GetHashCode() == ((UserDocument)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 956239593;
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

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IAddress))
                return false;

            return GetHashCode() == ((Address)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = -236468681;
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(City);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Line1);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Line2);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(State);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(ZipCode);
            return hashCode;
        }
    }

    public class Name : IName
    {
        #region PROPERTIES
        public string First { get; set; }

        public string Last { get; set; }

        public string Middle { get; set; }
        #endregion

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IName))
                return false;

            return GetHashCode() == ((Name)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 203425700;
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(First);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Last);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Middle);
            return hashCode;
        }
    }
}