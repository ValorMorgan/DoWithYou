using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;

namespace DoWithYou.Service
{
    public class ModelHandler<TModel, T1> : IModelHandler<TModel, T1>
        where TModel : IModel<T1>
        where T1 : IBaseDocument
    {
        #region VARIABLES
        private IModelRepository<TModel, T1> _repository;
        #endregion

        #region CONSTRUCTORS
        public ModelHandler(IModelRepository<TModel, T1> repository)
        {
            _repository = repository;
        }
        #endregion

        public void Delete(TModel model) =>
            _repository.Delete(model);

        public TModel Get(T1 document) =>
            _repository.Get(document);

        public TModel Get(Func<IQueryable<T1>, T1> request) =>
            _repository.Get(request);

        public IEnumerable<TModel> GetMany(IEnumerable<T1> documents) =>
            _repository.GetMany(documents)?.ToList() ?? new List<TModel>();

        public IEnumerable<TModel> GetMany(Func<IQueryable<T1>, IEnumerable<T1>> request) =>
            _repository.GetMany(request)?.ToList() ?? new List<TModel>();

        public void Insert(TModel model) =>
            _repository.Insert(model);

        public void SaveChanges() =>
            _repository.SaveChanges();

        public void Update(TModel model) =>
            _repository.Update(model);

        public void Dispose()
        {
            _repository?.Dispose();
            _repository = null;
        }
    }

    public class ModelHandler<TModel, T1, T2> : IModelHandler<TModel, T1, T2>
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
        #region VARIABLES
        private IModelRepository<TModel, T1, T2> _repository;
        #endregion

        #region CONSTRUCTORS
        public ModelHandler(IModelRepository<TModel, T1, T2> repository)
        {
            _repository = repository;
        }
        #endregion

        public void Delete(TModel model) =>
            _repository.Delete(model);

        public TModel Get((T1, T2) entity) =>
            _repository.Get(entity);

        public TModel Get(T1 entity1, T2 entity2) =>
            _repository.Get(entity1, entity2);

        public TModel Get<T>(Func<IQueryable<T>, T> request)
            where T : IBaseEntity =>
            _repository.Get(request);

        public IEnumerable<TModel> GetMany(IEnumerable<(T1, T2)> entities) =>
            _repository.GetMany(entities)?.ToList() ?? new List<TModel>();

        public IEnumerable<TModel> GetMany(IEnumerable<T1> entities1, IEnumerable<T2> entities2) =>
            _repository.GetMany(entities1, entities2)?.ToList() ?? new List<TModel>();

        public IEnumerable<TModel> GetMany<T>(Func<IQueryable<T>, IEnumerable<T>> request)
            where T : IBaseEntity =>
            _repository.GetMany(request);

        public void Insert(TModel model) =>
            _repository.Insert(model);

        public void SaveChanges() =>
            _repository.SaveChanges();

        public void Update(TModel model) =>
            _repository.Update(model);

        public void Dispose()
        {
            _repository?.Dispose();
            _repository = null;
        }
    }
}