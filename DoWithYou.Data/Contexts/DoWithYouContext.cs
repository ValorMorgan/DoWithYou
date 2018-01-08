using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Constants.SettingPaths;
using Microsoft.EntityFrameworkCore;

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
            _settings = settings;
        }

        public DoWithYouContext(IApplicationSettings settings, DbContextOptions<DoWithYouContext> options)
            : base(options)
        {
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

            string connectionStringSettingPath = ConnectionStrings.DoWithYouDB;
            string connectionString = _settings[connectionStringSettingPath];
            if (connectionString == default)
                throw new NullReferenceException($"Failed to connect to database. No connection string was provided at \"{connectionStringSettingPath}\".");

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
            builder.Entity<User>().ToTable("User");
            builder.Entity<UserProfile>().ToTable("UserProfile");
        }
        #endregion
    }
}