using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;

namespace DoWithYou.Model.Repositories.Models
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
            Get(entity.Item1, entity.Item2);

        public IUserModel Get(IUser entity1, IUserProfile entity2) =>
            entity1 != null ?
                Get<IUser>(e => e.FirstOrDefault(i => i.UserID == entity1.UserID)) :
            entity2 != null ?
                Get<IUserProfile>(e => e.FirstOrDefault(i => i.UserID == entity2.UserID)) :
                _mapper.MapEntityToModel(null, null);

        public IUserModel Get<T>(Func<IQueryable<T>, T> request)
            where T : IBaseEntity =>
            typeof(T) == typeof(IUser) ?
                Get(request as Func<IQueryable<IUser>, IUser>) :
            typeof(T) == typeof(IUserProfile) ?
                Get(request as Func<IQueryable<IUserProfile>, IUserProfile>) :
                null;

        public IUserModel Get(Func<IQueryable<IUser>, IUser> request)
        {
            var entity1 = _userRepository.Get(request);
            var entity2 = entity1 != null ?
                _userProfileRepository.Get(e => e.FirstOrDefault(i => i.UserID == entity1.UserID)) :
                default;

            return _mapper.MapEntityToModel(entity1, entity2);
        }

        public IUserModel Get(Func<IQueryable<IUserProfile>, IUserProfile> request)
        {
            var entity2 = _userProfileRepository.Get(request);
            var entity1 = entity2 != null ?
                _userRepository.Get(e => e.FirstOrDefault(i => i.UserID == entity2.UserID)) :
                default;

            return _mapper.MapEntityToModel(entity1, entity2);
        }

        public IEnumerable<IUserModel> GetMany(IEnumerable<(IUser, IUserProfile)> entities) =>
            entities.Select(Get);

        public IEnumerable<IUserModel> GetMany(IEnumerable<IUser> entities1, IEnumerable<IUserProfile> entities2)
        {
            // Enumerate
            var entities1List = entities1
                .OrderBy(e => e.UserID)
                .ToList();

            // Get filter (entities1 ID's)
            var ids = entities1List
                .Select(e => e.UserID);

            // Enumerate + apply filter
            var entities2List = entities2
                .Where(e => ids.Contains(e.UserID))
                .OrderBy(e => e.UserProfileID)
                .ToList();

            // Check same amount (1:1 mapping)
            if (entities1List.Count == entities2List.Count)
                return GetMany(entities1List.Zip(entities2List, (e1, e2) => (e1, e2)));

            // Differing amounts (some entities1 map to null)
            return entities1List.Select(e =>
                Get(e, entities2List
                    .FirstOrDefault(i => i.UserID == e.UserID))
            );
        }

        public IEnumerable<IUserModel> GetMany<T>(Func<IQueryable<T>, IEnumerable<T>> request)
            where T : IBaseEntity =>
            typeof(T) == typeof(IUser) ?
                GetMany(request as Func<IQueryable<IUser>, IEnumerable<IUser>>) :
            typeof(T) == typeof(IUserProfile) ?
                GetMany(request as Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>>) :
                null;

        public IEnumerable<IUserModel> GetMany(Func<IQueryable<IUser>, IEnumerable<IUser>> request)
        {
            var entities1 = _userRepository.GetMany(request).ToList();
            var ids = entities1.Select(i => i.UserID);

            var entities2 = _userProfileRepository.GetMany(e => e.Where(i => ids.Contains(i.UserID)));

            return GetMany(entities1, entities2);
        }

        public IEnumerable<IUserModel> GetMany(Func<IQueryable<IUserProfile>, IEnumerable<IUserProfile>> request)
        {
            var entities2 = _userProfileRepository.GetMany(request).ToList();
            var ids = entities2.Select(i => i.UserID);

            var entities1 = _userRepository.GetMany(e => e.Where(i => ids.Contains(i.UserID)));

            return GetMany(entities1, entities2);
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