using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Constants.SettingPaths;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Data.Contexts
{
    public class DoWithYouContext : DbContext, IDoWithYouContext
    {
        #region VARIABLES
        private readonly IApplicationSettings _settings;
        #endregion

        #region CONSTRUCTORS
        public DoWithYouContext(IApplicationSettings settings)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(DoWithYouContext));

            _settings = settings;
        }

        public DoWithYouContext(IApplicationSettings settings, DbContextOptions<DoWithYouContext> options)
            : base(options)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(DoWithYouContext));

            _settings = settings;
        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"Cannot configure Context with a NULL {nameof(DbContextOptionsBuilder)}.");

            if (builder.IsConfigured)
                return;

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Configuring {Class}", nameof(DoWithYouContext));

            string connectionString = _settings[ConnectionStrings.DoWithYouDB];
            if (connectionString == default)
                throw new NullReferenceException($"Failed to connect to database. No connection string was provided at \"{ConnectionStrings.DoWithYouDB}\".");

            Log.Logger.LogEventVerbose(
                LoggerEvents.DATA, 
                "{Class} to use SQL Server with {ConnectionString} connection string", 
                nameof(DoWithYouContext), nameof(ConnectionStrings.DoWithYouDB));
            builder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"Cannot create Context Model with a NULL {nameof(ModelBuilder)}.");

            UserMap.Map(builder.Entity<User>());
            UserProfileMap.Map(builder.Entity<UserProfile>());

            MapTableNames(builder);
        }

        #region PRIVATE
        private static void MapTableNames(ModelBuilder builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping table names for {Class}", nameof(DoWithYouContext));

            builder.Entity<User>().ToTable("User");
            builder.Entity<UserProfile>().ToTable("UserProfile");
        }
        #endregion
    }
}