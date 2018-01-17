using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog;

namespace DoWithYou.Data.Maps
{
    public static class UserMap
    {
        #region VARIABLES
        private static ILoggerTemplates TEMPLATES;
        #endregion

        public static void Map(EntityTypeBuilder<User> builder, ILoggerTemplates templates)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            TEMPLATES = templates;
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMap, nameof(User), nameof(EntityTypeBuilder));

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        #region PRIVATE
        private static void MapKeys(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapKeys, nameof(User), nameof(EntityTypeBuilder));

            builder.HasKey(e => e.UserId);
        }

        private static void MapProperties(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapProperties, nameof(User), nameof(EntityTypeBuilder));

            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Username).IsRequired();
        }

        private static void MapRelationships(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapRelationships, nameof(User), nameof(EntityTypeBuilder));

            builder.HasOne(e => e.UserProfile)
                .WithOne(e => e.User)
                .HasForeignKey<UserProfile>(e => e.UserId);
        }
        #endregion
    }
}