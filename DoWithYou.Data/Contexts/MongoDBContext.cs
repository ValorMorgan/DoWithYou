using System;
using System.Security.Authentication;
using DoWithYou.Interface.Model;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using MongoDB.Driver;
using Serilog;

namespace DoWithYou.Data.Contexts
{
    public class MongoDbContext
    {
        #region PROPERTIES
        public IMongoDatabase Database { get; }
        #endregion

        #region CONSTRUCTORS
        public MongoDbContext(string connectinoString, string database, bool isSsl = false)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(MongoDbContext));

            try
            {
                var settings = GetSettings(connectinoString, isSsl);
                Database = new MongoClient(settings).GetDatabase(database);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to access server \"{connectinoString}\".", ex);
            }
        }
        #endregion

        public void CreateCollection<TCollection>()
            where TCollection : IModel =>
            Database?.CreateCollection(typeof(TCollection).Name);

        public IMongoCollection<TDocument> GetCollection<TDocument>()
            where TDocument : IModel =>
            Database?.GetCollection<TDocument>(typeof(TDocument).Name);

        #region PRIVATE
        private static MongoClientSettings GetSettings(string connectinoString, bool isSsl = false)
        {
            var url = new MongoUrl(connectinoString);
            var settings = MongoClientSettings.FromUrl(url);
            if (settings == default)
                return default;

            if (isSsl)
            {
                settings.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = SslProtocols.Tls12
                };
            }

            return settings;
        }
        #endregion
    }
}