using System;
using DoWithYou.Interface.Data.Entity;

namespace DoWithYou.Data.Entities.DoWithYou.Base
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreationDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
