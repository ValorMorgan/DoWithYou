using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Entities.SQL.DoWithYou;
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
        private ModelHandler<IUserModel, IUser, IUserProfile> _handler;

        private ModelHandler<IUserModel, IUser, IUserProfile> Handler
        {
            get
            {
                if (_handler != null)
                    return _handler;

                var sub = Substitute.For<IModelRepository<IUserModel, IUser, IUserProfile>>();

                sub.When(x => x.Delete(Arg.Any<IUserModel>())).DoNotCallBase();
                sub.When(x => x.Insert(Arg.Any<IUserModel>())).DoNotCallBase();
                sub.When(x => x.Update(Arg.Any<IUserModel>())).DoNotCallBase();
                sub.When(x => x.SaveChanges()).DoNotCallBase();

                var newUser = new User();
                var newProfile = new UserProfile();
                var newModel = new UserModel();
                var newModelList = new List<IUserModel> { newModel };

                sub.Get(Arg.Any<(IUser, IUserProfile)>()).Returns(newModel);
                sub.Get(Arg.Any<IUser>(), Arg.Any<IUserProfile>()).Returns(newModel);
                sub.Get(Arg.Any<Func<IQueryable<IUser>, IUser>>()).Returns(newModel);
                sub.Get(Arg.Any<Func<IQueryable<IUserProfile>, IUserProfile>>()).Returns(newModel);
                sub.GetMany(Arg.Any<IEnumerable<(IUser, IUserProfile)>>()).Returns(newModelList);
                sub.GetMany(Arg.Any<IEnumerable<IUser>>(), Arg.Any<IEnumerable<IUserProfile>>()).Returns(newModelList);
                sub.GetMany(Arg.Any<Func<IQueryable<IUser>, IEnumerable<IUser>>>()).Returns(newModelList);
                sub.GetMany(Arg.Any<Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>>>()).Returns(newModelList);

                _handler = new ModelHandler<IUserModel, IUser, IUserProfile>(sub);

                return _handler;
            }
        }

        [Test]
        public void Get_When_Provided_Query_Returns_Default_Or_IUser()
        {
            Assert.That(Handler.Get((new User(), new UserProfile())), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(Handler.Get(new User(), new UserProfile()), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(Handler.Get<IUser>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
            Assert.That(Handler.Get<IUserProfile>(e => e.FirstOrDefault()), Is.InstanceOf<IUserModel>().And.Not.Null);
        }

        [Test]
        public void GetMany_When_Provided_Query_Returns_Default_Or_IUser()
        {
            Assert.That(Handler.GetMany(new List<(IUser, IUserProfile)>()), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            Assert.That(Handler.GetMany(new List<IUser>(), new List<IUserProfile>()), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            Assert.That(Handler.GetMany<IUser>(e => e), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
            Assert.That(Handler.GetMany<IUserProfile>(e => e), Is.InstanceOf<IEnumerable<IUserModel>>().And.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Delete_Throws_Nothing()
        {
            Assert.That(() => Handler.Delete(new UserModel()), Throws.Nothing);
        }

        [Test]
        public void Insert_Throws_Nothing()
        {
            Assert.That(() => Handler.Insert(new UserModel()), Throws.Nothing);
        }

        [Test]
        public void Update_Throws_Nothing()
        {
            Assert.That(() => Handler.Update(new UserModel()), Throws.Nothing);
        }

        [Test]
        public void Update_When_Provided_Query_Throws_Nothing()
        {
            Assert.That(() => Handler.Update(new UserModel()), Throws.Nothing);
        }

        [Test]
        public void SaveChanges_Throws_Nothing()
        {
            Assert.That(() => Handler.SaveChanges(), Throws.Nothing);
        }
    }
}
