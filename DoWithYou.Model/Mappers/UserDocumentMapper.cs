using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;

namespace DoWithYou.Model.Mappers
{
    public class UserDocumentMapper
    {
        public IUserModel MapDocumentToModel(IUserDocument document) =>
            GetModel(document);

        public (IUser, IUserProfile) MapDocumentToEntity(IUserDocument document) =>
            GetEntity(document);

        public IUserDocument MapEntityToDocument((IUser, IUserProfile) entity) =>
            MapEntityToDocument(entity.Item1, entity.Item2);

        public IUserDocument MapEntityToDocument(IUser entity1, IUserProfile entity2) =>
            GetDocument(entity1, entity2);

        #region PRIVATE

        private static IUserDocument GetDocument(IUser entity1, IUserProfile entity2) =>
            new UserDocument
            {
                ID = entity1?.UserID ?? default,
                Email = entity1?.Email,
                Password = entity1?.Password,
                Username = entity1?.Username,
                Phone = entity2?.Phone,
                Address = new Address
                {
                    Line1 = entity2?.Address1,
                    Line2 = entity2?.Address2,
                    City = entity2?.City,
                    State = entity2?.State,
                    ZipCode = entity2?.ZipCode
                },
                Name = new Name
                {
                    First = entity2?.FirstName,
                    Middle = entity2?.MiddleName,
                    Last = entity2?.LastName
                }
            };

        private static (IUser, IUserProfile) GetEntity(IUserDocument document) =>
            (GetUser(document), GetUserProfile(document));

        private static IUser GetUser(IUserDocument document) =>
            new User
            {
                UserID = document?.ID ?? default,
                Username = document?.Username,
                Email = document?.Email,
                Password = document?.Password
            };

        private static IUserProfile GetUserProfile(IUserDocument document) =>
            new UserProfile
            {
                UserProfileID = default, // TODO: No stored UserProfileID (what do?)
                UserID = document?.ID ?? default,
                FirstName = document?.Name?.First,
                MiddleName = document?.Name?.Middle,
                LastName = document?.Name?.Last,
                Address1 = document?.Address?.Line1,
                Address2 = document?.Address?.Line2,
                City = document?.Address?.City,
                State = document?.Address?.State,
                Phone = document?.Phone
            };
        #endregion
    }
}