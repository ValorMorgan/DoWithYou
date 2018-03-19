using System;
using DoWithYou.Data.Entities.NoSQL.DoWithYou;
using DoWithYou.Data.Entities.SQL.DoWithYou;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Model.Mappers;
using DoWithYou.Model.Models;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Model
{
    [TestFixture]
    public class UserModelMapperTests
    {
        private readonly IModelMapper<IUserModel, IUser, IUserProfile> _sqlMapper = new UserModelMapper();
        private readonly IModelMapper<IUserModel, IUserDocument> _noSqlMapper = new UserModelMapper();
        
        [Test]
        public void MapDocumentToModel_Returns_IUserModel_When_Given_Valid_Document()
        {
            var model = _noSqlMapper.MapDocumentToModel(TestEntities.UserDocument);
            
            Assert.That(model, Is.Not.Null.And.InstanceOf<IUserModel>());

            // We can't get UserProfileID from the document (document will never store extra ID's).
            model.UserProfileID = TestEntities.UserModel.UserProfileID;
            model.CreationDate = TestEntities.UserModel.CreationDate;
            model.ModifiedDate = TestEntities.UserModel.ModifiedDate;

            Assert.AreEqual(TestEntities.UserModel, model,
                $"{nameof(model)}: {model.GetHashCode()} != {nameof(TestEntities.UserModel)}: {TestEntities.UserModel.GetHashCode()}");
        }

        [Test]
        public void MapDocumentToModel_Returns_IUserModel_When_Given_Null_Document()
        {
            var model = _noSqlMapper.MapDocumentToModel(null);

            Assert.That(model, Is.Not.Null.And.InstanceOf<IUserModel>());

            var blankModel = new UserModel
            {
                CreationDate = model.CreationDate,
                ModifiedDate = model.ModifiedDate
            };

            Assert.AreEqual(blankModel, model,
                $"{nameof(model)}: {model.GetHashCode()} != {nameof(blankModel)}: {blankModel.GetHashCode()}");
        }

        [Test]
        public void MapEntityToModel_Returns_IUserModel_When_Given_Valid_Entities()
        {
            var modelFromTuple = _sqlMapper.MapEntityToModel((TestEntities.User, TestEntities.UserProfile));
            var modelFromParameters = _sqlMapper.MapEntityToModel(TestEntities.User, TestEntities.UserProfile);

            Assert.That(modelFromTuple, Is.Not.Null.And.InstanceOf<IUserModel>());
            Assert.That(modelFromParameters, Is.Not.Null.And.InstanceOf<IUserModel>());

            modelFromTuple.CreationDate = modelFromTuple.CreationDate;
            modelFromTuple.ModifiedDate = modelFromTuple.ModifiedDate;

            Assert.AreEqual(modelFromTuple, modelFromParameters,
                $"{nameof(modelFromTuple)}: {modelFromTuple.GetHashCode()} != {nameof(modelFromParameters)}: {modelFromParameters.GetHashCode()}");
        }

        [Test]
        public void MapEntityToModel_Returns_IUserModel_When_Given_Null_Entities()
        {
            var modelFromTuple = _sqlMapper.MapEntityToModel((null, null));
            var modelFromParameters = _sqlMapper.MapEntityToModel(null, null);

            Assert.That(modelFromTuple, Is.Not.Null.And.InstanceOf<IUserModel>());
            Assert.That(modelFromParameters, Is.Not.Null.And.InstanceOf<IUserModel>());

            modelFromTuple.CreationDate = modelFromTuple.CreationDate;
            modelFromTuple.ModifiedDate = modelFromTuple.ModifiedDate;

            Assert.AreEqual(modelFromTuple, modelFromParameters,
                $"{nameof(modelFromTuple)}: {modelFromTuple.GetHashCode()} != {nameof(modelFromParameters)}: {modelFromParameters.GetHashCode()}");
        }

        [Test]
        public void MapDocumentToModel_Returns_Document_When_Given_Valid_Model()
        {
            var document = _noSqlMapper.MapModelToDocument(TestEntities.UserModel);

            Assert.That(document, Is.Not.Null.And.InstanceOf<IUserDocument>());

            document.CreationDate = TestEntities.UserDocument.CreationDate;
            document.ModifiedDate = TestEntities.UserDocument.ModifiedDate;

            Assert.AreEqual(TestEntities.UserDocument, document,
                $"{nameof(document)}: {document.GetHashCode()} != {nameof(TestEntities.UserDocument)}: {TestEntities.UserDocument.GetHashCode()}");
        }

        [Test]
        public void MapDocumentToModel_Returns_Document_When_Given_Null_Model()
        {
            var document = _noSqlMapper.MapModelToDocument(null);

            Assert.That(document, Is.Not.Null.And.InstanceOf<IUserDocument>());
            
            var blankDocument = new UserDocument
            {
                CreationDate = document.CreationDate,
                ModifiedDate = document.ModifiedDate
            };

            Assert.AreEqual(blankDocument, document,
                $"{nameof(document)}: {document.GetHashCode()} != {nameof(blankDocument)}: {blankDocument.GetHashCode()}");
        }

        [Test]
        public void MapModelToEntity_Returns_Tuple_Of_Entities_When_Given_Valid_Model()
        {
            var entities = _sqlMapper.MapModelToEntity(TestEntities.UserModel);

            Assert.That(entities.Item1, Is.Not.Null.And.InstanceOf<IUser>());
            Assert.That(entities.Item2, Is.Not.Null.And.InstanceOf<IUserProfile>());
            
            entities.Item1.CreationDate = TestEntities.User.CreationDate;
            entities.Item1.ModifiedDate = TestEntities.User.ModifiedDate;
            entities.Item2.CreationDate = TestEntities.UserProfile.CreationDate;
            entities.Item2.ModifiedDate = TestEntities.UserProfile.ModifiedDate;

            Assert.AreEqual(TestEntities.User, entities.Item1,
                $"{nameof(entities.Item1)}: {entities.Item1.GetHashCode()} != {nameof(TestEntities.User)}: {TestEntities.User.GetHashCode()}");

            Assert.AreEqual(TestEntities.UserProfile, entities.Item2,
                $"{nameof(entities.Item2)}: {entities.Item2.GetHashCode()} != {nameof(TestEntities.UserProfile)}: {TestEntities.UserProfile.GetHashCode()}");
        }

        [Test]
        public void MapModelToEntity_Returns_Tuple_Of_Entities_When_Given_Null_Model()
        {
            var entities = _sqlMapper.MapModelToEntity(null);

            Assert.That(entities.Item1, Is.Not.Null.And.InstanceOf<IUser>());
            Assert.That(entities.Item2, Is.Not.Null.And.InstanceOf<IUserProfile>());

            User blankUser = new User();
            UserProfile blankUserProfile = new UserProfile();
            
            entities.Item1.CreationDate = blankUser.CreationDate;
            entities.Item1.ModifiedDate = blankUser.ModifiedDate;
            entities.Item2.CreationDate = blankUserProfile.CreationDate;
            entities.Item2.ModifiedDate = blankUserProfile.ModifiedDate;

            Assert.AreEqual(blankUser, entities.Item1,
                $"{nameof(entities.Item1)}: {entities.Item1.GetHashCode()} != {nameof(blankUser)}: {blankUser.GetHashCode()}");

            Assert.AreEqual(blankUserProfile, entities.Item2,
                $"{nameof(entities.Item2)}: {entities.Item2.GetHashCode()} != {nameof(blankUserProfile)}: {blankUserProfile.GetHashCode()}");
        }
    }
}
