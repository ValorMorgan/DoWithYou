using System;
using System.Linq;
using Autofac;
using DoWithYou.Data.Contexts;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Factories;
using DoWithYou.Shared.Repositories.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoWithYou.Data.Factories
{
    public class DoWithYouContextFactory : IDesignTimeDbContextFactory<DoWithYouContext>
    {
        /// <summary>Creates a new instance of a derived context.</summary>
        /// <param name="args"> Arguments provided by the design-time service. </param>
        /// <returns> An instance of DoWithYouContext. </returns>
        public DoWithYouContext CreateDbContext(string[] args)
        {
            try
            {
                using (var container = GetContainerBuilder().Build())
                using (var scope = container.BeginLifetimeScope())
                {
                    AppConfig appConfig = scope.Resolve<AppConfig>();
                    ILoggerTemplates loggerTemplates = scope.Resolve<ILoggerTemplates>();

                    DbContextOptionsBuilder<DoWithYouContext> dbOptionsBuilder = GetDbContextOptionsBuilder(appConfig);
                    if (dbOptionsBuilder == default(DbContextOptionsBuilder<DoWithYouContext>))
                        throw new ApplicationException($"Failed to setup the {nameof(DoWithYouContext)} Options Builder.");

                    return new DoWithYouContext(dbOptionsBuilder.Options, loggerTemplates);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to create a {nameof(DoWithYouContext)}", ex);
            }
        }

        #region PRIVATE
        private static ContainerBuilder GetContainerBuilder()
        {
            var configurationBuilderFactory = new ConfigurationBuilderFactory();
            var configurationBuilder = configurationBuilderFactory.GetBuilder();

            var containerFactory = new ContainerBuilderFactory();
            var builder = containerFactory.GetBuilder(configurationBuilder.Build());

            return builder;
        }

        private static DbContextOptionsBuilder<DoWithYouContext> GetDbContextOptionsBuilder(AppConfig appConfig)
        {
            string connectionString = appConfig.ConnectionStrings
                .Single(c => c?.Name == nameof(DoWithYou)).Connection;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new NullReferenceException($"Failed to retrieve a valid connection string from the provided {nameof(AppConfig)}.");

            return new DbContextOptionsBuilder<DoWithYouContext>()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(warningsBuilder => warningsBuilder.Default(WarningBehavior.Log));
        }
        #endregion
    }
}