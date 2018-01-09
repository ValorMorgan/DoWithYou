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

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} for {Class}", nameof(User), nameof(EntityTypeBuilder));

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        public static void MapKeys(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Keys for {Class}", nameof(User), nameof(EntityTypeBuilder));

            builder.HasKey(e => e.ID);
        }

        public static void MapProperties(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Properties for {Class}", nameof(User), nameof(EntityTypeBuilder));

            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Username).IsRequired();
        }

        public static void MapRelationships(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Relationships for {Class}", nameof(User), nameof(EntityTypeBuilder));

            builder.HasOne(e => e.UserProfile)
                .WithOne(e => e.User as User)
                .HasForeignKey<UserProfile>(e => e.ID);
        }
    }
}