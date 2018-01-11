using System.Collections.Generic;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Model.Repository;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserProfileRepositoryTests
    {
        private IRepository<UserProfile> MockedRepository
        {
            get
            {
                IRepository<UserProfile> substitute = Substitute.For<IRepository<UserProfile>>();

                substitute.Get(Arg.Any<long>()).Returns(new UserProfile());
                substitute.GetAll().Returns(new List<UserProfile>());
                substitute.When(r => r.Delete(Arg.Any<UserProfile>())).DoNotCallBase();
                substitute.When(r => r.Insert(Arg.Any<UserProfile>())).DoNotCallBase();
                substitute.When(r => r.Update(Arg.Any<UserProfile>())).DoNotCallBase();
                substitute.When(r => r.SaveChanges()).DoNotCallBase();

                return substitute;
            }
        }

        private UserProfileRepository TestRepository => new UserProfileRepository(null, MockedRepository, TestSetupFactory.GetLoggerTemplates());

        [Test]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void Get_Returns_One_Match(long id)
        {
            using (var repo = TestRepository)
                Assert.That(repo.Get(id), Is.Not.Null.And.InstanceOf<UserProfile>());
        }

        [Test]
        public void GetAll_Returns_Enumerable()
        {
            using (var repo = TestRepository)
                Assert.That(repo.GetAll(), Is.Not.Null.And.InstanceOf<IEnumerable<UserProfile>>());
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Delete(new UserProfile()), Throws.Nothing);
                Assert.That(() => repo.Delete(default(UserProfile)), Throws.Nothing);
                Assert.That(() => repo.Delete(null), Throws.Nothing);
            }
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Insert(new UserProfile()), Throws.Nothing);
                Assert.That(() => repo.Insert(default(UserProfile)), Throws.Nothing);
                Assert.That(() => repo.Insert(null), Throws.Nothing);
            }
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Update(new UserProfile()), Throws.Nothing);
                Assert.That(() => repo.Update(default(UserProfile)), Throws.Nothing);
                Assert.That(() => repo.Update(null), Throws.Nothing);
            }
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            using (var repo = TestRepository)
                Assert.That(() => repo.SaveChanges(), Throws.Nothing);
        }
    }
}
