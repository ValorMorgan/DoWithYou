using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Model
{
    public class UserModelRepository : IModelRepository<IUserModel, IUser, IUserProfile>
    {
        #region VARIABLES
        private readonly IModelMapper<IUserModel, IUser, IUserProfile> _mapper;
        private IRepository<IUserProfile> _userProfileRepository;
        private IRepository<IUser> _userRepository;
        #endregion

        #region CONSTRUCTORS
        public UserModelRepository(IRepository<IUser> userRepository, IRepository<IUserProfile> userProfileRepository, IModelMapper<IUserModel, IUser, IUserProfile> mapper)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }
        #endregion

        public void Delete(IUserModel model)
        {
            (IUser User, IUserProfile UserProfile) entities = _mapper.MapModelToEntity(model);
            _userRepository.Delete(entities.User);
            _userProfileRepository.Delete(entities.UserProfile);
        }

        public IUserModel Get((IUser, IUserProfile) entity) =>
            _mapper.MapEntityToModel(entity);

        public IUserModel Get(IUser entity1, IUserProfile entity2) =>
            Get((entity1, entity2));

        public IUserModel Get(Func<IQueryable<IUser>, IUser> request1, Func<IQueryable<IUserProfile>, IUserProfile> request2)
        {
            var user = _userRepository.Get(request1);
            var profile = _userProfileRepository.Get(request2);
            return Get(user, profile);
        }

        public IEnumerable<IUserModel> GetMany(IEnumerable<(IUser, IUserProfile)> entities) =>
            entities.Select(Get);

        public IEnumerable<IUserModel> GetMany(IEnumerable<IUser> entities1, IEnumerable<IUserProfile> entities2)
        {
            var entities = entities1.Zip(entities2, (e1, e2) => (e1, e2));
            return GetMany(entities);
        }

        public IEnumerable<IUserModel> GetMany(Func<IQueryable<IUser>, IEnumerable<IUser>> request1, Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>> request2)
        {
            var users = _userRepository.GetMany(request1);
            var profiles = _userProfileRepository.GetMany(request2);
            return GetMany(users, profiles);
        }

        public void Insert(IUserModel model)
        {
            (IUser, IUserProfile) entities = _mapper.MapModelToEntity(model);
            _userRepository.Insert(entities.Item1);
            _userProfileRepository.Insert(entities.Item2);
        }

        public void SaveChanges()
        {
            _userRepository.SaveChanges();
            _userProfileRepository.SaveChanges();
        }

        public void Update(IUserModel model)
        {
            (IUser, IUserProfile) entities = _mapper.MapModelToEntity(model);
            _userRepository.Update(entities.Item1);
            _userProfileRepository.Update(entities.Item2);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            _userRepository = null;

            _userProfileRepository?.Dispose();
            _userProfileRepository = null;
        }
    }
}