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
        private readonly IRepository<User> _mockedRepository = Substitute.For<IRepository<User>>();

        private IRepository<User> MockedRepository
        {
            get
            {
                _mockedRepository.Get(Arg.Any<long>()).Returns(new User());
                _mockedRepository.GetAll().Returns(new List<User>());
                _mockedRepository.When(r => r.Delete(Arg.Any<User>())).DoNotCallBase();
                _mockedRepository.When(r => r.Insert(Arg.Any<User>())).DoNotCallBase();
                _mockedRepository.When(r => r.Update(Arg.Any<User>())).DoNotCallBase();
                _mockedRepository.When(r => r.SaveChanges()).DoNotCallBase();

                return _mockedRepository;
            }
        }

        private UserRepository TestRepository => new UserRepository(MockedRepository);

        [Test]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void Get_Returns_One_Match(long id)
        {
            Assert.That(TestRepository.Get(id), Is.Not.Null.And.InstanceOf<User>());
        }

        [Test]
        public void GetAll_Returns_Enumerable()
        {
            Assert.That(TestRepository.GetAll(), Is.Not.Null.And.InstanceOf<IEnumerable<User>>());
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Delete(new User()), Throws.Nothing);
            Assert.That(() => TestRepository.Delete(default(User)), Throws.Nothing);
            Assert.That(() => TestRepository.Delete(null), Throws.Nothing);
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Insert(new User()), Throws.Nothing);
            Assert.That(() => TestRepository.Insert(default(User)), Throws.Nothing);
            Assert.That(() => TestRepository.Insert(null), Throws.Nothing);
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Update(new User()), Throws.Nothing);
            Assert.That(() => TestRepository.Update(default(User)), Throws.Nothing);
            Assert.That(() => TestRepository.Update(null), Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() => TestRepository.SaveChanges(), Throws.Nothing);
        }
    }
}
