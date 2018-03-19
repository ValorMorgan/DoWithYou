using System;

namespace DoWithYou.Interface.Entity
{
    public interface IBaseDocument
    {
        DateTime CreationDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
