namespace DoWithYou.Interface.Entity
{
    public interface IUserDocument : IBaseDocument
    {
        IAddress Address { get; set; }

        string Email { get; set; }

        long ID { get; set; }

        IName Name { get; set; }

        string Password { get; set; }

        string Phone { get; set; }

        string Username { get; set; }
    }

    public interface IAddress
    {
        string City { get; set; }

        string Line1 { get; set; }

        string Line2 { get; set; }

        string State { get; set; }

        string ZipCode { get; set; }
    }

    public interface IName
    {
        string First { get; set; }

        string Last { get; set; }

        string Middle { get; set; }
    }
}