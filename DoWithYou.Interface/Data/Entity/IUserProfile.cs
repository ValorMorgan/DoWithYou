namespace DoWithYou.Interface.Data.Entity
{
    public interface IUserProfile : IBaseUserEntity
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string Phone { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
    }
}