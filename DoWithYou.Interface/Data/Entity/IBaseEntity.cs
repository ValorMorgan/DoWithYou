using System;

namespace DoWithYou.Interface.Data.Entity
{
    public interface IBaseEntity
    {
        DateTime CreationDate { get; set; }

        DateTime ModifiedDate { get; set; }
    }
}