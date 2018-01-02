using System;

namespace DoWithYou.Data.Entities.DoWithYou.Base
{
    public abstract class BaseEntity
    {
        #region PROPERTIES
        public DateTime CreationDate { get; set; }

        public long ID { get; set; }

        public DateTime ModifiedDate { get; set; }
        #endregion
    }
}