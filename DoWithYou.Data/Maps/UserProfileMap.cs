using System;
using DoWithYou.Data.Entities.DoWithYou;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoWithYou.Data.Maps
{
    public static class UserProfileMap
    {
        public static void Map(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            MapKeys(builder);
            MapProperties(builder);
            MapRelationships(builder);
        }

        public static void MapKeys(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

            builder.HasKey(e => e.ID);
        }

        public static void MapProperties(EntityTypeBuilder<UserProfile> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), $"{nameof(EntityTypeBuilder)} cannot be NULL.");

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

            builder.HasOne(e => e.User)
                .WithOne(e => e.UserProfile)
                .HasForeignKey<User>(e => e.ID);
        }
    }
}