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
$@"{{
    {nameof(ID)}: {ID},
    {nameof(Username)}: {Username},
    {nameof(Password)}: {Password},
    {nameof(Email)}: {Email},
    {nameof(Address)}: {{
        {nameof(Address.Line1)}: {Address.Line1},
        {nameof(Address.Line2)}: {Address.Line2},
        {nameof(Address.City)}: {Address.City},
        {nameof(Address.State)}: {Address.State},
        {nameof(Address.ZipCode)}: {Address.ZipCode}
    }},
    {nameof(Name)}: {{
        {nameof(Name.First)}: {Name.First},
        {nameof(Name.Middle)}: {Name.Middle},
        {nameof(Name.Last)}: {Name.Last}
    }},
    {nameof(Phone)}: {Phone},
    {base.ToString()}
}}";

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