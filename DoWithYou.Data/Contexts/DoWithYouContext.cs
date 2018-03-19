using System;
using DoWithYou.Data.Entities.SQL.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Data.Contexts
{
    public class DoWithYouContext : DbContext
    {
        #region CONSTRUCTORS
        public DoWithYouContext(DbContextOptions<DoWithYouContext> options)
            : base(options)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.CONSTRUCTOR, nameof(DoWithYouContext));
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"Cannot create Context Model with a null {nameof(ModelBuilder)}.");

            UserMap.Map(builder.Entity<User>());
            UserProfileMap.Map(builder.Entity<UserProfile>());

            MapTableNames(builder);
        }

        #region PRIVATE
        private static void MapTableNames(ModelBuilder builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DATA_MAP_TABLES, nameof(DoWithYouContext));

            builder.Entity<User>().ToTable("User");
            builder.Entity<UserProfile>().ToTable("UserProfile");
        }
        #endregion
    }
}