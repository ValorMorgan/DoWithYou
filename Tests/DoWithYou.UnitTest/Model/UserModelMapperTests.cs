using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Mappers;
using DoWithYou.Model.Models;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserModelMapperTests
    {
        private readonly IModelMapper<IUserModel, IUser, IUserProfile> _mapper = new UserModelMapper();

        private static readonly IUser _testUser = new User()
        {
            UserID = 1,
            Username = "testUser",
            Password = "test",
            Email = "test@test.com"
        };

        private static readonly IUserProfile _testUserProfile = new UserProfile()
        {
            UserProfileID = 1,
            UserID = 1,
            FirstName = "Test First",
            MiddleName = "Test Middle",
            LastName = "Test Last",
            Address1 = "9999 Test St.",
            Address2 = "Apt. T1",
            City = "Test City",
            State = "TE",
            ZipCode = "99999",
            Phone = "999-999-9999"
        };

        [Test]
        public void MapEntityToModel_Returns_IUserModel_When_Given_Valid_Entities()
        {
            IUserModel modelFromTuple = _mapper.MapEntityToModel((_testUser, _testUserProfile));
            IUserModel modelFromParameters = _mapper.MapEntityToModel(_testUser, _testUserProfile);

            Assert.That(modelFromTuple, Is.Not.Null.And.InstanceOf<IUserModel>());
            Assert.That(modelFromParameters, Is.Not.Null.And.InstanceOf<IUserModel>());

            Assert.AreEqual(modelFromTuple, modelFromParameters,
                $"{nameof(modelFromTuple)}: {modelFromTuple.GetHashCode()} != {nameof(modelFromParameters)}: {modelFromParameters.GetHashCode()}");
        }

        [Test]
        public void MapEntityToModel_Returns_IUserModel_When_Given_Null_Entities()
        {
            IUserModel modelFromTuple = _mapper.MapEntityToModel((null, null));
            IUserModel modelFromParameters = _mapper.MapEntityToModel(null, null);

            Assert.That(modelFromTuple, Is.Not.Null.And.InstanceOf<IUserModel>());
            Assert.That(modelFromParameters, Is.Not.Null.And.InstanceOf<IUserModel>());

            Assert.AreEqual(modelFromTuple, modelFromParameters,
                $"{nameof(modelFromTuple)}: {modelFromTuple.GetHashCode()} != {nameof(modelFromParameters)}: {modelFromParameters.GetHashCode()}");
        }

        [Test]
        public void MapModelToEntity_Returns_Tuple_Of_Entities_When_Given_Valid_Model()
        {
            var entities = _mapper.MapModelToEntity(new UserModel(_testUser, _testUserProfile));

            Assert.That(entities.Item1, Is.Not.Null.And.InstanceOf<IUser>());
            Assert.That(entities.Item2, Is.Not.Null.And.InstanceOf<IUserProfile>());

            Assert.AreEqual(entities.Item1, _testUser,
                $"{nameof(entities.Item1)}: {entities.Item1.GetHashCode()} != {nameof(_testUser)}: {_testUser.GetHashCode()}");

            Assert.AreEqual(entities.Item2, _testUserProfile,
                $"{nameof(entities.Item2)}: {entities.Item2.GetHashCode()} != {nameof(_testUserProfile)}: {_testUserProfile.GetHashCode()}");
        }

        [Test]
        public void MapModelToEntity_Returns_Tuple_Of_Entities_When_Given_A_Null_Model()
        {
            var entities = _mapper.MapModelToEntity(null);

            Assert.That(entities.Item1, Is.Not.Null.And.InstanceOf<IUser>());
            Assert.That(entities.Item2, Is.Not.Null.And.InstanceOf<IUserProfile>());

            User newUser = new User();
            UserProfile newUserProfile = new UserProfile();

            Assert.AreEqual(entities.Item1, newUser,
                $"{nameof(entities.Item1)}: {entities.Item1.GetHashCode()} != {nameof(newUser)}: {newUser.GetHashCode()}");

            Assert.AreEqual(entities.Item2, newUserProfile,
                $"{nameof(entities.Item2)}: {entities.Item2.GetHashCode()} != {nameof(newUserProfile)}: {newUserProfile.GetHashCode()}");
        }
    }
}
