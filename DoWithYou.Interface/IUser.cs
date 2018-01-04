namespace DoWithYou.Interface
{
    public interface IUser : IBaseEntity
    {
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        IUserProfile UserProfile { get; set; }
    }
}