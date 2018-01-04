using System.Collections.Generic;
using DoWithYou.Data;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface;

namespace DoWithYou.Model.Repository
{
    public class UserProfileRepository : IRepository<IUserProfile>
    {
        #region VARIABLES
        private DoWithYouContext _context = new DoWithYouContext();
        private IRepository<UserProfile> _repository;
        #endregion

        #region CONSTRUCTORS
        public UserProfileRepository()
        {
            _repository = new Repository<UserProfile>(_context);
        }
        #endregion

        public void Delete(IUserProfile entity) => _repository.Delete((UserProfile)entity);

        public IUserProfile Get(long id) => _repository.Get(id);

        public IEnumerable<IUserProfile> GetAll() => _repository.GetAll();

        public void Insert(IUserProfile entity) => _repository.Insert((UserProfile)entity);

        public void SaveChanges() => _repository.SaveChanges();

        public void Update(IUserProfile entity) => _repository.Update((UserProfile)entity);
        
        public void Dispose()
        {
            _repository?.Dispose();
            _repository = null;

            _context?.Dispose();
            _context = null;
        }
    }
}