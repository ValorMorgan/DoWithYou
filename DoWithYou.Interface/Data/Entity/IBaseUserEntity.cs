using System;

namespace DoWithYou.Interface.Data.Entity
{
    public interface IBaseUserEntity
    {
        DateTime CreationDate { get; set; }

        DateTime ModifiedDate { get; set; }

        long UserId { get; set; }
    }
}