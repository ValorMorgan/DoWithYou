﻿using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface.Data;
using DoWithYou.Model.Base;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class EntityRepositoryTests
    {
        private static readonly BaseEntity[] TEST_CASES = {new BaseEntity(), default, null};

        private class BaseEntityRepository : EntityRepository<BaseEntity>, IRepository<BaseEntity>
        {
            #region CONSTRUCTORS
            public BaseEntityRepository(IDoWithYouContext context)
                : base(context) { }

            public BaseEntityRepository(IDoWithYouContext context, DbSet<BaseEntity> entiities)
                : base(context, entiities) { }
            #endregion
        }

        private IDoWithYouContext MockedContext
        {
            get
            {
                var sub = Substitute.For<IDoWithYouContext>();

                sub.When(x => x.SaveChanges()).DoNotCallBase();
                //sub.When(x => x.Set<BaseEntity>().Returns(MockedDbSet)).DoNotCallBase();

                return sub;
            }
        }

        private DbSet<BaseEntity> MockedDbSet
        {
            get
            {
                var sub = Substitute.For<DbSet<BaseEntity>, IQueryable<BaseEntity>>()
                    .Initialize(TEST_CASES.AsQueryable());

                sub.When(x => x.Add(Arg.Any<BaseEntity>())).DoNotCallBase();
                sub.When(x => x.AddRange(Arg.Any<BaseEntity[]>())).DoNotCallBase();
                sub.When(x => x.Remove(Arg.Any<BaseEntity>())).DoNotCallBase();
                sub.When(x => x.Update(Arg.Any<BaseEntity>())).DoNotCallBase();

                return sub;
            }
        }

        private EntityRepository<BaseEntity> Repository => new BaseEntityRepository(MockedContext, MockedDbSet);

        [Test]
        [TestCaseSource(nameof(TEST_CASES))]
        public void Delete_Throws_Nothing(BaseEntity arg)
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
                Assert.That(repo.Get(e => e.FirstOrDefault(i => i != null)), Is.InstanceOf<BaseEntity>().And.Not.Null);
        }

        [Test]
        public void GetMany_Returns_Expected_Results()
        {
            using (var repo = Repository)
                Assert.That(repo.GetMany(e => e.Where(i => i != null)), Is.InstanceOf<IEnumerable<BaseEntity>>().And.Not.Null.And.Not.Empty);
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
        public void Insert_Throws_Nothing(BaseEntity arg)
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
        public void Update_Throws_Nothing(BaseEntity arg)
        {
            Assert.That(() =>
            {
                using (var repo = Repository) repo.Update(arg);
            }, Throws.Nothing);
        }
    }
}