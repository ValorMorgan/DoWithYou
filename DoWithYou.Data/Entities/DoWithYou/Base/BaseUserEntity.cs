using DoWithYou.Interface.Data.Entity;

namespace DoWithYou.Data.Entities.DoWithYou.Base
{
    public abstract class BaseUserEntity : BaseEntity, IBaseUserEntity
    {
        public long UserId { get; set; }
    }
}