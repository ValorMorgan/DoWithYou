using System;
using DoWithYou.Interface.Data.Entity;

namespace DoWithYou.Data.Entities.DoWithYou.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public long ID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}