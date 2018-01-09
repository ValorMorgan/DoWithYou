using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog;

namespace DoWithYou.Data.Maps
{
    public static class UserProfileMap
    {
        public static void Map(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} for {Class}", nameof(UserProfile), nameof(EntityTypeBuilder));

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        public static void MapKeys(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Keys for {Class}", nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.HasKey(e => e.ID);
        }

        public static void MapProperties(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Properties for {Class}", nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.Address1).IsRequired();
            builder.Property(e => e.City).IsRequired();
            builder.Property(e => e.State).IsRequired();
            builder.Property(e => e.ZipCode).IsRequired();
        }

        public static void MapRelationships(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            Log.Logger.LogEventVerbose(LoggerEvents.DATA, "Mapping {Table} Relationships for {Class}", nameof(UserProfile), nameof(EntityTypeBuilder));

            builder.HasOne(e => e.User)
                .WithOne(e => e.UserProfile as UserProfile)
                .HasForeignKey<User>(e => e.ID);
        }
    }
}