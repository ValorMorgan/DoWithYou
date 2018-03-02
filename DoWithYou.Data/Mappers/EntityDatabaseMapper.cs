using System;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Factories;
using DoWithYou.Interface.Entity;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Repositories.Settings;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Mappers
{
    public class EntityDatabaseMapper<T> : IEntityDatabaseMapper<T>
        where T : IBaseEntity
    {
        #region VARIABLES
        private readonly AppConfig _config;
        #endregion

        #region CONSTRUCTORS
        public EntityDatabaseMapper(AppConfig config)
        {
            _config = config;
        }
        #endregion

        public DbContext MapEntityToContext()
        {
            var type = typeof(T);

            switch (type)
            {
                case Type _ when type == typeof(IUser):
                case Type _ when type == typeof(IUserProfile):
                    return GetDoWithYouContext();

                default:
                    throw new NotImplementedException("Entity is not mapped to a context yet.");
            }
        }

        #region PRIVATE
        private string GetConnectionString(string name) =>
            _config.ConnectionStrings
                .Single(c => c?.Name == name)
                ?.Connection;

        private DbContext GetDoWithYouContext()
        {
            var factory = new DbContextOptionsFactory<DoWithYouContext>();
            return new DoWithYouContext(factory.GetOptions(GetConnectionString(ConnectionStringNames.DO_WITH_YOU)));
        }
        #endregion
    }
}