using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog;

namespace DoWithYou.Data.Maps
{
    public static class UserProfileMap
    {
        #region VARIABLES
        private static ILoggerTemplates TEMPLATES;
        #endregion

        public static void Map(EntityTypeBuilder<UserProfile> builder, ILoggerTemplates templates)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            TEMPLATES = templates;
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMap, nameof(UserProfile), nameof(EntityTypeBuilder));

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        #region PRIVATE
        private static void MapKeys(EntityTypeBuilder<UserProfile> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapKeys, nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.HasKey(e => e.ID);
        }

        private static void MapProperties(EntityTypeBuilder<UserProfile> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapProperties, nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.Address1).IsRequired();
            builder.Property(e => e.City).IsRequired();
            builder.Property(e => e.State).IsRequired();
            builder.Property(e => e.ZipCode).IsRequired();
        }

        private static void MapRelationships(EntityTypeBuilder<UserProfile> builder)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.DATA, TEMPLATES.DataMapRelationships, nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.HasOne(e => e.User)
                .WithOne(e => e.UserProfile as UserProfile)
                .HasForeignKey<User>(e => e.ID);
        }
        #endregion
    }
}