using System;
using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
{
    public interface IUserModel : IModel<IUserDocument>, IModel<IUser, IUserProfile>
    {
        IAddress Address { get; set; }

        DateTime? CreationDate { get; set; }

        string Email { get; set; }

        long? UserID { get; set; }

        long? UserProfileID { get; set; }

        DateTime? ModifiedDate { get; set; }

        IName Name { get; set; }

        string Password { get; set; }

        string Phone { get; set; }

        string Username { get; set; }
    }
}