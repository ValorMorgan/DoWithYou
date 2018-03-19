using System;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Data.Entities.SQL.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;

namespace DoWithYou.UnitTest
{
    static class TestEntities
    {
        #region PROPERTIES
        public static IUser User { get; } = new User
        {
            UserID = 1,
            Username = "testUser",
            Password = "test",
            Email = "test@test.com",
            CreationDate = DateTime.Today,
            ModifiedDate = null
        };

        public static IUserProfile UserProfile { get; } = new UserProfile
        {
            UserProfileID = 1,
            UserID = User.UserID,
            FirstName = "Test First",
            MiddleName = "Test Middle",
            LastName = "Test Last",
            Address1 = "9999 Test St.",
            Address2 = "Apt. T1",
            City = "Test City",
            State = "TE",
            ZipCode = "99999",
            Phone = "999-999-9999",
            CreationDate = User.CreationDate,
            ModifiedDate = User.ModifiedDate
        };

        public static IUserModel UserModel { get; } = new UserModel
        {
            Address = new Address
            {
                Line1 = UserProfile.Address1,
                Line2 = UserProfile.Address2,
                City = UserProfile.City,
                State = UserProfile.State,
                ZipCode = UserProfile.ZipCode
            },
            Email = User.Email,
            UserID = User.UserID,
            UserProfileID = UserProfile.UserProfileID,
            Name = new Name
            {
                First = UserProfile.FirstName,
                Middle = UserProfile.MiddleName,
                Last = UserProfile.LastName
            },
            Password = User.Password,
            Phone = UserProfile.Phone,
            Username = User.Username,
            CreationDate = User.CreationDate,
            ModifiedDate = User.ModifiedDate
        };

        public static IUserDocument UserDocument { get; } = new UserDocument
        {
            Address = UserModel.Address,
            Email = UserModel.Email,
            ID = UserModel.UserID ?? default,
            Name = UserModel.Name,
            Password = UserModel.Password,
            Phone = UserModel.Phone,
            Username = UserModel.Username,
            CreationDate = UserModel.CreationDate ?? DateTime.Today,
            ModifiedDate = UserModel.ModifiedDate
        };
        #endregion
    }
}