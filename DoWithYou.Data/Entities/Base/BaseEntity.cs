using System;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Entity.SQL;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;

namespace DoWithYou.Data.Entities.Base
{
    public class BaseEntity : IBaseEntity, IBaseDocument
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public override string ToString() =>
            Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public override bool Equals(object obj)
        {
            if (!(obj is IBaseEntity) || !(obj is IBaseDocument))
                return false;

            return GetHashCode() == ((BaseEntity)obj).GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = nameof(BaseEntity).GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + CreationDate.TruncateToSecond().GetHashCode();
            hashCode = hashCode * HashConstants.MULTIPLIER + (ModifiedDate?.TruncateToSecond().GetHashCode() ?? 0);
            return hashCode;
        }
    }
}
