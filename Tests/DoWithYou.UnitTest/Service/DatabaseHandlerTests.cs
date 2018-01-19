using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Service;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Service
{
    [TestFixture]
    public class DatabaseHandlerTests
    {
        private DatabaseHandler<IUser> _queryGenerator;

        private DatabaseHandler<IUser> TestingGenerator
        {
            get
            {
                if (_queryGenerator != null)
                    return _queryGenerator;

                IRepository<IUser> repository = Substitute.For<IRepository<IUser>>();
                
                repository.GetAll().Returns(new List<IUser>());
                repository.When(x => x.Delete(Arg.Any<IUser>())).DoNotCallBase();
                repository.When(x => x.Insert(Arg.Any<IUser>())).DoNotCallBase();
                repository.When(x => x.Update(Arg.Any<IUser>())).DoNotCallBase();
                repository.When(x => x.SaveChanges()).DoNotCallBase();

                _queryGenerator = new DatabaseHandler<IUser>(repository, TestSetupFactory.GetLoggerTemplates());

                return _queryGenerator;
            }
        }

        [Test]
        public void Get_When_Provided_Query_Returns_Default_Or_IUser()
        {
            Assert.That(TestingGenerator.Get(u => u != null), Is.EqualTo(default(IUser)).Or.InstanceOf<IUser>());
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            IUser user = new User();
            Assert.That(() => TestingGenerator.Delete(user), Throws.Nothing);
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            IUser user = new User();
            Assert.That(() => TestingGenerator.Insert(user), Throws.Nothing);
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            IUser user = new User();
            Assert.That(() => TestingGenerator.Update(user), Throws.Nothing);
        }

        [Test]
        public void Update_When_Provided_Query_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.Update(u => u != null), Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.SaveChanges(), Throws.Nothing);
        }
    }
}
