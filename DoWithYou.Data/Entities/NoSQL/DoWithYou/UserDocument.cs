using DoWithYou.Data.Entities.Base;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;
using MongoDB.Bson.Serialization.Attributes;

namespace DoWithYou.Data.Entities.NoSQL.DoWithYou
{
    public class UserDocument : BaseEntity, IUserDocument
    {
        [BsonId]
        public long ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public IName Name { get; set; }

        public IAddress Address { get; set; }

        public string Phone { get; set; }

        public IToDo[] ToDos { get; set; }

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
            int hashCode = nameof(UserDocument).GetHashCode();
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
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

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
            int hashCode = nameof(Address).GetHashCode();
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
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

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
            int hashCode = nameof(Name).GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(First);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Last);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Middle);
            return hashCode;
        }
    }

    public class ToDo : IToDo
    {
        public string Name { get; set; }

        public bool Complete { get; set; }

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IToDo))
                return false;

            return GetHashCode() == ((ToDo)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = nameof(ToDo).GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Name);
            hashCode = hashCode * HashConstants.MULTIPLIER + Complete.GetHashCode();
            return hashCode;
        }
    }
}