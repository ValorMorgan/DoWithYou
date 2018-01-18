using System.Collections.Generic;
using DoWithYou.Data;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Model.Repository
{
    public class UserProfileRepository : IRepository<IUserProfile>
    {
        #region VARIABLES
        private IDoWithYouContext _context;
        private IRepository<UserProfile> _repository;
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public UserProfileRepository(IDoWithYouContext context, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, nameof(UserProfileRepository));

            _context = context;
            _repository = new Repository<UserProfile>(_context, templates);
        }

        internal UserProfileRepository(IDoWithYouContext context, IRepository<UserProfile> repository, ILoggerTemplates templates)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, nameof(UserProfileRepository));

            _context = context;
            _repository = repository;
        }
        #endregion

        public void Delete(IUserProfile entity) => _repository.Delete((UserProfile)entity);

        public IEnumerable<IUserProfile> GetAll() => _repository.GetAll();

        public void Insert(IUserProfile entity) => _repository.Insert((UserProfile)entity);

        public void SaveChanges() => _repository.SaveChanges();

        public void Update(IUserProfile entity) => _repository.Update((UserProfile)entity);

        public void Dispose()
        {
            Log.Logger.LogEventDebug(LoggerEvents.DISPOSE, _templates.Dispose, nameof(UserProfileRepository));

            _repository?.Dispose();
            _repository = null;

            _context?.Dispose();
            _context = null;
        }
    }
}