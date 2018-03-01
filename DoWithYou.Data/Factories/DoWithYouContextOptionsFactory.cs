using System;
using DoWithYou.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Factories
{
    public class DoWithYouContextOptionsFactory
    {
        public DbContextOptions<DoWithYouContext> GetOptions(string connectionString) =>
            GetDbContextOptionsBuilder(connectionString)?.Options;

        public DbContextOptionsBuilder<DoWithYouContext> GetDbContextOptionsBuilder(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return new DbContextOptionsBuilder<DoWithYouContext>()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(warningsBuilder => warningsBuilder.Default(WarningBehavior.Log));
        }
    }
}
