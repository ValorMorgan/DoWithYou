using System;
using System.Linq;
using DoWithYou.Interface.Service;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Service
{
    public class ModelRequestor<T1, TModel> : IModelRequestor<T1, TModel>
    {
        #region VARIABLES
        private IModelMapper<TModel, T1> _mapper;
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public ModelRequestor(IModelMapper<TModel, T1> mapper, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, GetType()?.Name);

            _mapper = mapper;
        }
        #endregion

        public TModel GetModelFromEntity(T1 entity) => _mapper.MapEntityToModel(entity);

        public TModel RequestModel(Func<IQueryable<T1>, T1> request) => _mapper.RequestModel(request);

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, _templates.Dispose, GetType()?.Name);

            _mapper?.Dispose();
            _mapper = null;
        }
    }

    public class ModelRequestor<T1, T2, TModel> : IModelRequestor<T1, T2, TModel>
    {
        #region VARIABLES
        private IModelMapper<TModel, T1, T2> _mapper;
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public ModelRequestor(IModelMapper<TModel, T1, T2> mapper, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, GetType()?.Name);

            _mapper = mapper;
        }
        #endregion

        public TModel GetModelFromEntity(T1 entity) => _mapper.MapEntityToModel(entity);

        public TModel GetModelFromEntity(T2 entity) => _mapper.MapEntityToModel(entity);

        public TModel GetModelFromEntity(T1 entity1, T2 entity2) => _mapper.MapEntityToModel(entity1, entity2);

        public TModel RequestModel(Func<IQueryable<T1>, T1> request) => _mapper.RequestModel(request);

        public TModel RequestModel(Func<IQueryable<T2>, T2> request) => _mapper.RequestModel(request);

        public TModel RequestModel(Func<IQueryable<T1>, T1> request1, Func<IQueryable<T2>, T2> request2) => _mapper.RequestModel(request1, request2);

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, _templates.Dispose, GetType()?.Name);

            _mapper?.Dispose();
            _mapper = null;
        }
    }
}