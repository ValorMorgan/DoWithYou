using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Interface.Service
{
    public interface IUserModelMapper
    {
        IUserModel GetUserModel(IUser user, IUserProfile profile);

        IUser MapUserModelToUser(IUserModel model);

        IUserProfile MapUserModelToUserProfile(IUserModel model);

        IUserModel MapUserProfileToUserModel(IUserProfile profile);

        IUserModel MapUserToUserModel(IUser user);
    }
}