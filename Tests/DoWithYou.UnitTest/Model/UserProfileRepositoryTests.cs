using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Repositories.Entities;
using DoWithYou.Data.Repositories.Entities.Base;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserProfileRepositoryTests
    {
        private static readonly UserProfile[] TEST_CASES = {new UserProfile(), default, null};

        private DbContext MockedContext
        {
            get
            {
                var sub = Substitute.For<DbContext>();

                sub.When(x => x.SaveChanges()).DoNotCallBase();
                //sub.When(x => x.Set<UserProfile>().Returns(MockedDbSet)).DoNotCallBase();

                return sub;
            }
        }

        private DbSet<UserProfile> MockedDbSet
        {
            get
            {
                var sub = Substitute.For<DbSet<UserProfile>, IQueryable<UserProfile>>()
                    .Initialize(TEST_CASES.AsQueryable());

                sub.When(x => x.Add(Arg.Any<UserProfile>())).DoNotCallBase();
                sub.When(x => x.AddRange(Arg.Any<UserProfile[]>())).DoNotCallBase();
                sub.When(x => x.Remove(Arg.Any<UserProfile>())).DoNotCallBase();
                sub.When(x => x.Update(Arg.Any<UserProfile>())).DoNotCallBase();

                return sub;
            }
        }

        private EntityRepository<UserProfile> Repository => new UserProfileRepository(MockedContext, MockedDbSet);

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Delete_Throws_Nothing(UserProfile arg)
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.Delete(arg);
            }, Throws.Nothing);
        }

        [Test]
        public void Get_Returns_Expected_Results()
        {
            using (var repo = Repository)
                Assert.That(repo.Get(e => e.FirstOrDefault(i => i != null)), Is.InstanceOf<UserProfile>().And.Not.Null);
        }

        [Test]
        public void GetMany_Returns_Expected_Results()
        {
            using (var repo = Repository)
                Assert.That(repo.GetMany(e => e.Where(i => i != null)), Is.InstanceOf<IEnumerable<UserProfile>>().And.Not.Null.And.Not.Empty);
        }

        [Test]
        public void GetQueryable_Returns_Not_Null()
        {
            using (var repo = Repository)
            {
                Assert.That(repo.GetQueryable(), Is.Not.Null);
                Assert.That(() => repo.GetQueryable().ToList(), Throws.Nothing);
            }
        }

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Insert_Throws_Nothing(UserProfile arg)
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
        public void Update_Throws_Nothing(UserProfile arg)
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.Update(arg);
            }, Throws.Nothing);
        }
    }
}