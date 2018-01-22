using System;

namespace DoWithYou.Interface.Entity
{
    public interface IBaseEntity
    {
        DateTime CreationDate { get; set; }

        DateTime ModifiedDate { get; set; }
    }
}