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
        private readonly IRepository<UserProfile> _mockedRepository = Substitute.For<IRepository<UserProfile>>();

        private IRepository<UserProfile> MockedRepository
        {
            get
            {
                _mockedRepository.Get(Arg.Any<long>()).Returns(new UserProfile());
                _mockedRepository.GetAll().Returns(new List<UserProfile>());
                _mockedRepository.When(r => r.Delete(Arg.Any<UserProfile>())).DoNotCallBase();
                _mockedRepository.When(r => r.Insert(Arg.Any<UserProfile>())).DoNotCallBase();
                _mockedRepository.When(r => r.Update(Arg.Any<UserProfile>())).DoNotCallBase();
                _mockedRepository.When(r => r.SaveChanges()).DoNotCallBase();

                return _mockedRepository;
            }
        }

        private UserProfileRepository TestRepository => new UserProfileRepository(MockedRepository);

        [Test]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        public void Get_Returns_One_Match(long id)
        {
            Assert.That(TestRepository.Get(id), Is.Not.Null.And.InstanceOf<UserProfile>());
        }

        [Test]
        public void GetAll_Returns_Enumerable()
        {
            Assert.That(TestRepository.GetAll(), Is.Not.Null.And.InstanceOf<IEnumerable<UserProfile>>());
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Delete(new UserProfile()), Throws.Nothing);
            Assert.That(() => TestRepository.Delete(default(UserProfile)), Throws.Nothing);
            Assert.That(() => TestRepository.Delete(null), Throws.Nothing);
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Insert(new UserProfile()), Throws.Nothing);
            Assert.That(() => TestRepository.Insert(default(UserProfile)), Throws.Nothing);
            Assert.That(() => TestRepository.Insert(null), Throws.Nothing);
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            Assert.That(() => TestRepository.Update(new UserProfile()), Throws.Nothing);
            Assert.That(() => TestRepository.Update(default(UserProfile)), Throws.Nothing);
            Assert.That(() => TestRepository.Update(null), Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() => TestRepository.SaveChanges(), Throws.Nothing);
        }
    }
}
