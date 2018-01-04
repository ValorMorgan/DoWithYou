using System;

namespace DoWithYou.Interface
{
    public interface IBaseEntity
    {
        DateTime CreationDate { get; set; }
        long ID { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}