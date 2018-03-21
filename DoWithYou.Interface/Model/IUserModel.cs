using System;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Entity.SQL;

namespace DoWithYou.Interface.Model
{
    public interface IUserModel : IModel<IUserDocument>, IModel<IUser, IUserProfile>
    {
        IAddress Address { get; set; }

        DateTime? CreationDate { get; set; }

        string Email { get; set; }

        DateTime? ModifiedDate { get; set; }

        IName Name { get; set; }

        string Password { get; set; }

        string Phone { get; set; }

        long? UserID { get; set; }

        string Username { get; set; }

        long? UserProfileID { get; set; }
    }
}