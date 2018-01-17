using System;

namespace DoWithYou.Interface.Data.Entity
{
    public interface IUser : IBaseUserEntity
    {
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}