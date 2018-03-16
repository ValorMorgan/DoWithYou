using System;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Interface.Model;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using DoWithYou.Shared.Repositories.Settings;
using Serilog;

namespace DoWithYou.Data.Mappers
{
    public class CollectionDatabaseMapper<T> : ICollectionDatabaseMapper<T>
        where T : IModel
    {
        #region VARIABLES
        private readonly AppConfig _config;
        #endregion

        #region CONSTRUCTORS
        public CollectionDatabaseMapper(AppConfig config)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, $"{nameof(CollectionDatabaseMapper<T>)}<{typeof(T).Name}>");
            _config = config;
        }
        #endregion

        public MongoDbContext MapCollectionToContext()
        {
            var type = typeof(T);

            switch (type)
            {
                case Type _ when type == typeof(IUserModel):
                    return GetDoWithYouContext();

                // TODO: Add database discovery somehow (or default database choice)?
                default:
                    throw new NotImplementedException($"Collection \"{typeof(T).Name}\" is not mapped to a context yet.");
            }
        }

        #region PRIVATE
        private string GetConnectionString(string name) =>
            _config.ConnectionStrings
                .Single(c => c?.Name == name)
                ?.Connection;

        private MongoDbContext GetDoWithYouContext()
        {
            string connectionString = GetConnectionString(ConnectionStringNames.DO_WITH_YOU);
            return new MongoDbContext(connectionString, ConnectionStringNames.DO_WITH_YOU);
        }
        #endregion
    }
}