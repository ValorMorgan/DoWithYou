﻿using System.Collections.Generic;
using DoWithYou.Data;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;

namespace DoWithYou.Model.Repository
{
    public class UserRepository : IRepository<IUser>
    {
        #region VARIABLES
        private DoWithYouContext _context = new DoWithYouContext();
        private IRepository<User> _repository;
        #endregion

        #region CONSTRUCTORS
        public UserRepository()
        {
            _repository = new Repository<User>(_context);
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
            _repository?.Dispose();
            _repository = null;

            _context?.Dispose();
            _context = null;
        }
    }
}