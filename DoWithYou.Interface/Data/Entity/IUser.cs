using System;

namespace DoWithYou.Interface.Data.Entity
{
    public interface IUser : IBaseEntity
    {
        string Email { get; set; }

        string Password { get; set; }

        long UserID { get; set; }

        string Username { get; set; }
    }
}