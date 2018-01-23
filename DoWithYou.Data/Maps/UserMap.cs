using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog;

namespace DoWithYou.Data.Maps
{
    public static class UserMap
    {
        public static void Map(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");
            
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DataMap, nameof(User), nameof(EntityTypeBuilder));

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        #region PRIVATE
        private static void MapKeys(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DataMapKeys, nameof(User), nameof(EntityTypeBuilder));

            builder.HasKey(e => e.UserID);
        }

        private static void MapProperties(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DataMapProperties, nameof(User), nameof(EntityTypeBuilder));

            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Username).IsRequired();
        }

        private static void MapRelationships(EntityTypeBuilder<User> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, LoggerTemplates.DataMapRelationships, nameof(User), nameof(EntityTypeBuilder));

            builder.HasOne(e => e.UserProfile)
                .WithOne(e => e.User)
                .HasForeignKey<UserProfile>(e => e.UserID);
        }
        #endregion
    }
}