using DoWithYou.Interface.Entity;

namespace DoWithYou.Interface.Model
{
    public interface IUserModel : IModel<IUser, IUserProfile>
    {
        string Address1 { get; set; }

        string Address2 { get; set; }

        string City { get; set; }

        string Email { get; set; }

        string FirstName { get; set; }

        string FullAddress { get; }

        string FullName { get; }

        string LastName { get; set; }

        string MiddleName { get; set; }

        string Phone { get; set; }

        string State { get; set; }

        string Username { get; set; }

        string ZipCode { get; set; }
    }
}