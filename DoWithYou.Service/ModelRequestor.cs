using System;
using System.Linq;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;

namespace DoWithYou.Service
{
    public class ModelRequestor<TModel, T1> : IModelRequestor<TModel, T1>
        where TModel : IModel<T1>
        where T1 : IBaseEntity
    {
        #region VARIABLES
        private readonly IDatabaseHandler<T1> _handler1;
        private readonly IModelMapper<TModel, T1> _mapper;
        #endregion

        #region CONSTRUCTORS
        public ModelRequestor(IModelMapper<TModel, T1> mapper, IDatabaseHandler<T1> handler1)
        {
            _mapper = mapper;
            _handler1 = handler1;
        }
        #endregion

        public TModel GetModel(Func<IQueryable<T1>, T1> request1) =>
            GetModel(_handler1.Get(request1));

        public TModel GetModel(T1 entity1) =>
            _mapper.MapEntityToModel(entity1);
    }

    public class ModelRequestor<TModel, T1, T2> : IModelRequestor<TModel, T1, T2>
        where TModel : IModel<T1, T2>
        where T1 : IBaseEntity
        where T2 : IBaseEntity
    {
        #region VARIABLES
        private readonly IDatabaseHandler<T1> _handler1;
        private readonly IDatabaseHandler<T2> _handler2;
        private readonly IModelMapper<TModel, T1, T2> _mapper;
        #endregion

        #region CONSTRUCTORS
        public ModelRequestor(IModelMapper<TModel, T1, T2> mapper, IDatabaseHandler<T1> handler1, IDatabaseHandler<T2> handler2)
        {
            _mapper = mapper;
            _handler1 = handler1;
            _handler2 = handler2;
        }
        #endregion

        public TModel GetModel(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2) =>
            GetModel(_handler1.Get(request1), _handler2.Get(request2));

        public TModel GetModel(T1 entity1, T2 entity2) =>
            _mapper.MapEntityToModel(entity1, entity2);
    }
}