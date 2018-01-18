using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DoWithYou.Data.Contexts
{
    public class DoWithYouContext : DbContext, IDoWithYouContext
    {
        #region VARIABLES
        private readonly ILoggerTemplates _templates;
        #endregion

        #region CONSTRUCTORS
        public DoWithYouContext(DbContextOptions<DoWithYouContext> options, ILoggerTemplates templates)
            : base(options)
        {
            _templates = templates;

            Log.Logger.LogEventDebug(LoggerEvents.CONSTRUCTOR, _templates.Constructor, nameof(DoWithYouContext));
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"Cannot create Context Model with a null {nameof(ModelBuilder)}.");

            UserMap.Map(builder.Entity<User>(), _templates);
            UserProfileMap.Map(builder.Entity<UserProfile>(), _templates);

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