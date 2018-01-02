using System;
using DoWithYou.Data.Entities.DoWithYou;
using DoWithYou.Data.Maps;
using DoWithYou.Shared;
using DoWithYou.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Contexts
{
    public class DoWithYouContext : DbContext
    {
        #region CONSTRUCTORS
        public DoWithYouContext(DbContextOptions<DoWithYouContext> options)
            : base(options) { }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), "Cannot configure Context with a NULL DbContextOptionsBuilder.");

            if (builder.IsConfigured)
                return;
            
            builder.UseSqlServer(Resolver.Resolve<ApplicationSettings>()[Shared.Constants.SettingPaths.ConnectionStrings.DefaultConnection]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), "Cannot create Context Model with a NULL ModelBuilder.");

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