using System;
using System.Collections.Generic;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Model.Repository;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IRepository<User> MockedRepository
        {
            get
            {
                IRepository<User> substitute = Substitute.For<IRepository<User>>();

                substitute.Get(Arg.Any<long>()).Returns(new User());
                substitute.GetAll().Returns(new List<User>());
                substitute.When(r => r.Delete(Arg.Any<User>())).DoNotCallBase();
                substitute.When(r => r.Insert(Arg.Any<User>())).DoNotCallBase();
                substitute.When(r => r.Update(Arg.Any<User>())).DoNotCallBase();
                substitute.When(r => r.SaveChanges()).DoNotCallBase();

                return substitute;
            }
        }

        private UserRepository TestRepository => new UserRepository(null, MockedRepository, TestSetupFactory.GetLoggerTemplates());

        [Test]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void Get_Returns_One_Match(long id)
        {
            using (var repo = TestRepository)
                Assert.That(repo.Get(id), Is.Not.Null.And.InstanceOf<User>());
        }

        [Test]
        public void GetAll_Returns_Enumerable()
        {
            using (var repo = TestRepository)
                Assert.That(repo.GetAll(), Is.Not.Null.And.InstanceOf<IEnumerable<User>>());
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Delete(new User()), Throws.Nothing);
                Assert.That(() => repo.Delete(default(User)), Throws.Nothing);
                Assert.That(() => repo.Delete(null), Throws.Nothing);
            }
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Insert(new User()), Throws.Nothing);
                Assert.That(() => repo.Insert(default(User)), Throws.Nothing);
                Assert.That(() => repo.Insert(null), Throws.Nothing);
            }
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            using (var repo = TestRepository)
            {
                Assert.That(() => repo.Update(new User()), Throws.Nothing);
                Assert.That(() => repo.Update(default(User)), Throws.Nothing);
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
