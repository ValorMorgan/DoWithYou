using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Data.Contexts
{
    public class DoWithYouContext : DbContext, IDoWithYouContext
    {
        #region CONSTRUCTORS
        public DoWithYouContext(DbContextOptions<DoWithYouContext> options)
            : base(options)
        {
            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, LoggerTemplates.Constructor, nameof(DoWithYouContext));
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
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DataMapTables, nameof(DoWithYouContext));

            builder.Entity<User>().ToTable("User");
            builder.Entity<UserProfile>().ToTable("UserProfile");
        }
        #endregion
    }
}