using System;
using DoWithYou.Data.Entities.Base;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Converters;

namespace DoWithYou.Data.Entities.SQL.DoWithYou
{
    public class UserProfile : BaseEntity, IUserProfile
    {
        public long UserProfileID { get; set; }

        public long UserID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public virtual User User { get; set; }

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IUserProfile))
                return false;

            return GetHashCode() == ((UserProfile)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 61574393;
            hashCode = hashCode * HashConstants.MULTIPLIER + base.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + UserProfileID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + UserID.GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(FirstName);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(MiddleName);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(LastName);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Phone);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Address1);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(Address2);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(City);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(State);
            hashCode = hashCode * HashConstants.MULTIPLIER + StringConverter.ToHash(ZipCode);
            return hashCode;
        }
    }
}