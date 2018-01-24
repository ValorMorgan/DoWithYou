using System;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;

namespace DoWithYou.Data.Entities.DoWithYou.Base
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is IBaseEntity))
                return false;

            return GetHashCode() == ((BaseEntity)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = 1212440406;
            hashCode = hashCode * HashConstants.MULTIPLIER + CreationDate.TruncateToSecond().GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + (ModifiedDate?.TruncateToSecond().GetHashCode() ?? 0);
            return hashCode;
        }
    }
}
