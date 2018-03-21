using System;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Factories;
using DoWithYou.Interface.Entity.SQL;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, $"{nameof(EntityDatabaseMapper<T>)}<{typeof(T).Name}>");
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

                // TODO: Add database discovery somehow (or default database choice)?
                default:
                    throw new NotImplementedException($"Entity \"{typeof(T).Name}\" is not mapped to a context yet.");
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
            string connectionString = GetConnectionString(ConnectionStringNames.DO_WITH_YOU);
            return new DoWithYouContext(factory.GetOptions(connectionString));
        }
        #endregion
    }
}