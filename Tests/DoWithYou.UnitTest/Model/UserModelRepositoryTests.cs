using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model;
using DoWithYou.Model.Models;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserModelRepositoryTests
    {
        private static readonly UserModel[] TEST_CASES =
        {
            new UserModel(new User(), new UserProfile()),
            new UserModel(null, null),
            default,
            null
        };

        public IRepository<IUser> MockedUserRepository
        {
            get
            {
                var sub = Substitute.For<IRepository<IUser>>();

                sub.When(x => x.Delete(Arg.Any<IUser>())).DoNotCallBase();
                sub.When(x => x.Insert(Arg.Any<IUser>())).DoNotCallBase();
                sub.When(x => x.Update(Arg.Any<IUser>())).DoNotCallBase();
                sub.When(x => x.SaveChanges()).DoNotCallBase();

                var newUser = new User();
                var newUserList = new List<IUser> {newUser};

                sub.Get(Arg.Any<Func<IQueryable<IUser>, IUser>>()).Returns(newUser);
                sub.GetMany(Arg.Any<Func<IQueryable<IUser>, IEnumerable<IUser>>>()).Returns(newUserList);

                return sub;
            }
        }

        public IRepository<IUserProfile> MockedUserProfileRepository
        {
            get
            {
                var sub = Substitute.For<IRepository<IUserProfile>>();

                sub.When(x => x.Delete(Arg.Any<IUserProfile>())).DoNotCallBase();
                sub.When(x => x.Insert(Arg.Any<IUserProfile>())).DoNotCallBase();
                sub.When(x => x.Update(Arg.Any<IUserProfile>())).DoNotCallBase();
                sub.When(x => x.SaveChanges()).DoNotCallBase();

                var newUserProfile = new UserProfile();
                var newUserProfileList = new List<IUserProfile> {newUserProfile};

                sub.Get(Arg.Any<Func<IQueryable<IUserProfile>, IUserProfile>>()).Returns(newUserProfile);
                sub.GetMany(Arg.Any<Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>>>()).Returns(newUserProfileList);

                return sub;
            }
        }

        private IModelMapper<IUserModel, IUser, IUserProfile> MockedMapper
        {
            get
            {
                var sub = Substitute.For<IModelMapper<IUserModel, IUser, IUserProfile>>();

                var newUser = new User();
                var newProfile = new UserProfile();
                var newModel = new UserModel(newUser, newProfile);

                sub.MapEntityToModel(Arg.Any<(IUser, IUserProfile)>()).Returns(newModel);
                sub.MapEntityToModel(Arg.Any<IUser>(), Arg.Any<IUserProfile>()).Returns(newModel);
                sub.MapModelToEntity(Arg.Any<IUserModel>()).Returns((newUser, newProfile));

                return sub;
            }
        }

        private UserModelRepository Repository => new UserModelRepository(MockedUserRepository, MockedUserProfileRepository, MockedMapper);

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Delete_Throws_Nothing(IUserModel arg)
        {
            Assert.That(() => Repository.Delete(arg), Throws.Nothing);
        }

        [Theory]
        public void Get_Returns_Expected_Results()
        {
            using (var repo = MockedUserRepository)
            {
                if (repo.Get(e => e.FirstOrDefault()) == null)
                    Assert.Inconclusive($"{nameof(MockedUserRepository)} not returning correct values.");
            }

            using (var repo = MockedUserProfileRepository)
            {
                if (repo.Get(e => e.FirstOrDefault()) == null)
                    Assert.Inconclusive($"{nameof(MockedUserProfileRepository)} not returning correct values.");
            }

            using (var repo = Repository)
            {
                Assert.That(repo.Get<IUser>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
                Assert.That(repo.Get<IUserProfile>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
            }
        }

        [Theory]
        public void GetMany_Returns_Expected_Results()
        {
            using (var repo = MockedUserRepository)
            {
                if (!repo.GetMany(e => e)?.Any() ?? true)
                    Assert.Inconclusive($"{nameof(MockedUserRepository)} not returning correct values.");
            }

            using (var repo = MockedUserProfileRepository)
            {
                if (!repo.GetMany(e => e)?.Any() ?? true)
                    Assert.Inconclusive($"{nameof(MockedUserProfileRepository)} not returning correct values.");
            }

            using (var repo = Repository)
            {
                Assert.That(repo.GetMany<IUser>(e => e), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
                Assert.That(repo.GetMany<IUserProfile>(e => e), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            }
        }

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Insert_Throws_Nothing(IUserModel arg)
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.Insert(arg);
            }, Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.SaveChanges();
            }, Throws.Nothing);
        }

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Update_Throws_Nothing(IUserModel arg)
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.Update(arg);
            }, Throws.Nothing);
        }
    }
}