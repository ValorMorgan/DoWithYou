using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity.NoSQL;
using DoWithYou.Interface.Model;

namespace DoWithYou.Model.Repositories.NoSQL
{
    public class UserModelRepository : IModelRepository<IUserModel, IUserDocument>
    {
        #region VARIABLES
        private readonly IModelMapper<IUserModel, IUserDocument> _mapper;
        private IRepository<IUserDocument> _userDocumentRepository;
        #endregion

        #region CONSTRUCTORS
        public UserModelRepository(IRepository<IUserDocument> userDocumentRepository, IModelMapper<IUserModel, IUserDocument> mapper)
        {
            _userDocumentRepository = userDocumentRepository;
            _mapper = mapper;
        }
        #endregion

        public void Delete(IUserModel model)
        {
            IUserDocument document = _mapper.MapModelToDocument(model);
            _userDocumentRepository.Delete(document);
        }
        
        public IUserModel Get(IUserDocument document) => document != null ?
            Get(e => e.FirstOrDefault(i => i.ID == document.ID)) :
            _mapper.MapDocumentToModel(null);

        public IUserModel Get(Func<IQueryable<IUserDocument>, IUserDocument> request)
        {
            var document = _userDocumentRepository.Get(request);
            return _mapper.MapDocumentToModel(document);
        }
        
        public IEnumerable<IUserModel> GetMany(IEnumerable<IUserDocument> documents) =>
            documents.Select(Get);
        
        public IEnumerable<IUserModel> GetMany(Func<IQueryable<IUserDocument>, IEnumerable<IUserDocument>> request)
        {
            var documents = _userDocumentRepository.GetMany(request).ToList();
            return GetMany(documents);
        }
        
        public void Insert(IUserModel model)
        {
            IUserDocument document = _mapper.MapModelToDocument(model);
            _userDocumentRepository.Insert(document);
        }

        public void SaveChanges()
        {
            _userDocumentRepository.SaveChanges();
        }

        public void Update(IUserModel model)
        {
            IUserDocument document = _mapper.MapModelToDocument(model);
            _userDocumentRepository.Update(document);
        }

        public void Dispose()
        {
            _userDocumentRepository?.Dispose();
            _userDocumentRepository = null;
        }
    }
}