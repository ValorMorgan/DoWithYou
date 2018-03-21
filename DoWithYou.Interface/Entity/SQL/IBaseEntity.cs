using System;

namespace DoWithYou.Interface.Entity.SQL
{
    public interface IBaseEntity
    {
        DateTime CreationDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}