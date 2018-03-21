using System;

namespace DoWithYou.Interface.Entity.NoSQL
{
    public interface IBaseDocument
    {
        DateTime CreationDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
