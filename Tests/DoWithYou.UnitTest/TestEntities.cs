using System;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Data.Entities.SQL.DoWithYou;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Entity.SQL;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;

namespace DoWithYou.UnitTest
{
    static class TestEntities
    {
        #region MODEL
        public static IUserModel UserModel { get; } = new UserModel
        {
            Address = new Address
            {
                Line1 = SQL_UserProfile.Address1,
                Line2 = SQL_UserProfile.Address2,
                City = SQL_UserProfile.City,
                State = SQL_UserProfile.State,
                ZipCode = SQL_UserProfile.ZipCode
            },
            Email = SQL_User.Email,
            UserID = SQL_User.UserID,
            UserProfileID = SQL_UserProfile.UserProfileID,
            Name = new Name
            {
                First = SQL_UserProfile.FirstName,
                Middle = SQL_UserProfile.MiddleName,
                Last = SQL_UserProfile.LastName
            },
            Password = SQL_User.Password,
            Phone = SQL_UserProfile.Phone,
            Username = SQL_User.Username,
            CreationDate = SQL_User.CreationDate,
            ModifiedDate = SQL_User.ModifiedDate
        };
        #endregion

        #region SQL
        public static IUser SQL_User { get; } = new User
        {
            UserID = 1,
            Username = "testUser",
            Password = "test",
            Email = "test@test.com",
            CreationDate = DateTime.Today,
            ModifiedDate = null
        };

        public static IUserProfile SQL_UserProfile { get; } = new UserProfile
        {
            UserProfileID = 1,
            UserID = SQL_User.UserID,
            FirstName = "Test First",
            MiddleName = "Test Middle",
            LastName = "Test Last",
            Address1 = "9999 Test St.",
            Address2 = "Apt. T1",
            City = "Test City",
            State = "TE",
            ZipCode = "99999",
            Phone = "999-999-9999",
            CreationDate = SQL_User.CreationDate,
            ModifiedDate = SQL_User.ModifiedDate
        };
        #endregion

        #region NoSQL
        public static IAddress NoSQL_Address { get; } = UserModel.Address;

        public static IName NoSQL_Name { get; } = UserModel.Name;

        public static IToDo NoSQL_ToDo { get; } = new DoWithYou.Data.Entities.NoSQL.DoWithYou.ToDo()
        {
            Name = "Test ToDo",
            Complete = false
        };

        public static IUserDocument NoSQL_UserDocument { get; } = new UserDocument
        {
            Address = NoSQL_Address,
            Email = UserModel.Email,
            ID = UserModel.UserID ?? default,
            Name = NoSQL_Name,
            Password = UserModel.Password,
            Phone = UserModel.Phone,
            ToDos = new [] { NoSQL_ToDo },
            Username = UserModel.Username,
            CreationDate = UserModel.CreationDate ?? DateTime.Today,
            ModifiedDate = UserModel.ModifiedDate
        };
        #endregion
    }
}