using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Models;
using DoWithYou.Service;
using NSubstitute;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Service
{
    [TestFixture]
    public class ModelHandlerTests
    {
        private ModelHandler<IUserModel, IUser, IUserProfile> _queryGenerator;

        private ModelHandler<IUserModel, IUser, IUserProfile> TestingGenerator
        {
            get
            {
                if (_queryGenerator != null)
                    return _queryGenerator;

                IModelRepository<IUserModel, IUser, IUserProfile> repository = Substitute.For<IModelRepository<IUserModel, IUser, IUserProfile>>();
                IUser newUser = new User();
                IUserProfile newProfile = new UserProfile();
                IUserModel newModel = new UserModel(newUser, newProfile);
                IList<IUserModel> newModelList = new List<IUserModel> { newModel };

                repository.When(x => x.Get(Arg.Any<(IUser, IUserProfile)>()).Returns(newModel));
                repository.When(x => x.Get(Arg.Any<IUser>(), Arg.Any<IUserProfile>()).Returns(newModel));
                repository.When(x => x.Get(Arg.Any<Func<IQueryable<IUser>, IUser>>()).Returns(newModel));
                repository.When(x => x.Get(Arg.Any<Func<IQueryable<IUserProfile>, IUserProfile>>()).Returns(newModel));
                repository.When(x => x.GetMany(Arg.Any<IEnumerable<(IUser, IUserProfile)>>()).Returns(newModelList));
                repository.When(x => x.GetMany(Arg.Any<IEnumerable<IUser>>(), Arg.Any<IEnumerable<IUserProfile>>()).Returns(newModelList));
                repository.When(x => x.GetMany(Arg.Any<Func<IQueryable<IUser>, IEnumerable<IUser>>>()).Returns(newModelList));
                repository.When(x => x.GetMany(Arg.Any<Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>>>()).Returns(newModelList));
                repository.When(x => x.Delete(Arg.Any<IUserModel>())).DoNotCallBase();
                repository.When(x => x.Insert(Arg.Any<IUserModel>())).DoNotCallBase();
                repository.When(x => x.Update(Arg.Any<IUserModel>())).DoNotCallBase();
                repository.When(x => x.SaveChanges()).DoNotCallBase();

                _queryGenerator = new ModelHandler<IUserModel, IUser, IUserProfile>(repository);

                return _queryGenerator;
            }
        }

        [Test]
        public void Get_When_Provided_Query_Returns_Default_Or_IUser()
        {
            Assert.That(TestingGenerator.Get((new User(), new UserProfile())), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(TestingGenerator.Get(new User(), new UserProfile()), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(TestingGenerator.Get<IUser>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(TestingGenerator.Get<IUserProfile>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
        }

        [Test]
        public void GetMany_When_Provided_Query_Returns_Default_Or_IUser()
        {
            Assert.That(TestingGenerator.GetMany(new List<(IUser, IUserProfile)>()), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            Assert.That(TestingGenerator.GetMany(new List<IUser>(), new List<IUserProfile>()), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            Assert.That(TestingGenerator.GetMany<IUser>(e => e.Where(i => i != null)), Is.InstanceOf<IUserModel>().And.Not.Null.And.Not.Empty);
            Assert.That(TestingGenerator.GetMany<IUserProfile>(e => e.Where(i => i != null)), Is.InstanceOf<IUserModel>().And.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.Delete(new UserModel(null, null)), Throws.Nothing);
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.Insert(new UserModel(null, null)), Throws.Nothing);
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.Update(new UserModel(null, null)), Throws.Nothing);
        }

        [Test]
        public void Update_When_Provided_Query_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.Update(new UserModel(null, null)), Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() => TestingGenerator.SaveChanges(), Throws.Nothing);
        }
    }
}
