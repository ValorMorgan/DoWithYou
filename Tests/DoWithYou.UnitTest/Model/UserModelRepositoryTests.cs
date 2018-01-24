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

                sub.When(x => x.Get(Arg.Any<Func<IQueryable<IUser>, IUser>>()).Returns(new User()));
                sub.When(x => x.GetMany(Arg.Any<Func<IQueryable<IUser>, IEnumerable<IUser>>>()).Returns(new List<IUser> {new User()}));

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

                sub.When(x => x.Get(Arg.Any<Func<IQueryable<IUserProfile>, IUserProfile>>()).Returns(new UserProfile()));
                sub.When(x => x.GetMany(Arg.Any<Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>>>()).Returns(new List<IUserProfile> { new UserProfile() }));

                return sub;
            }
        }

        private IModelMapper<IUserModel, IUser, IUserProfile> MockedMapper
        {
            get
            {
                var sub = Substitute.For<IModelMapper<IUserModel, IUser, IUserProfile>>();

                sub.When(x => x.MapEntityToModel(Arg.Any<(IUser, IUserProfile)>()).Returns(new UserModel(new User(), new UserProfile())));
                sub.When(x => x.MapEntityToModel(Arg.Any<IUser>(), Arg.Any<IUserProfile>()).Returns(new UserModel(new User(), new UserProfile())));
                sub.When(x => x.MapModelToEntity(Arg.Any<IUserModel>()).Returns((new User(), new UserProfile())));

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

        [Test]
        public void Get_Returns_Expected_Results()
        {
            using (var repo = Repository)
            {
                Assert.That(repo.Get(
                        e => e.FirstOrDefault(i => i != null),
                        e => e.FirstOrDefault(i => i != null)),
                    Is.InstanceOf<IUserModel>().And.Not.Null);
            }
        }

        [Test]
        public void GetMany_Returns_Expected_Results()
        {
            using (var repo = Repository)
            {
                Assert.That(repo.GetMany(
                        e => e.Where(i => i != null),
                        e => e.Where(i => i != null)),
                    Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
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