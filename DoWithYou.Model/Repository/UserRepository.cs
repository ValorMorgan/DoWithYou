using System.Collections.Generic;
using DoWithYou.Data;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Model.Repository
{
    public class UserRepository : IRepository<IUser>
    {
        #region VARIABLES
        private IDoWithYouContext _context;
        private IRepository<User> _repository;
        #endregion

        #region CONSTRUCTORS
        public UserRepository(IDoWithYouContext context)
        {
            Log.Logger.LogEventInformation(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(UserRepository));

            _context = context;
            _repository = new Repository<User>(_context);
        }

        internal UserRepository(IDoWithYouContext context, IRepository<User> repository)
        {
            Log.Logger.LogEventInformation(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(UserRepository));

            _context = context;
            _repository = repository;
        }
        #endregion

        public void Delete(IUser entity) => _repository.Update((User)entity);

        public IUser Get(long id) => _repository.Get(id);

        public IEnumerable<IUser> GetAll() => _repository.GetAll();

        public void Insert(IUser entity) => _repository.Insert((User)entity);

        public void SaveChanges() => _repository.SaveChanges();

        public void Update(IUser user) => _repository.Update((User)user);

        public void Dispose()
        {
            Log.Logger.LogEventInformation(LoggerEvents.DISPOSE, "Disposing {Class}", nameof(UserRepository));

            _repository?.Dispose();
            _repository = null;

            _context?.Dispose();
            _context = null;
        }
    }
}