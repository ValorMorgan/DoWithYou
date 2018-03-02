using System;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Factories
{
    public class DbContextOptionsFactory<T> : IDbContextOptionsFactory<T>
        where T : DbContext
    {
        public DbContextOptions<T> GetOptions(string connectionString) =>
            GetDbContextOptionsBuilder(connectionString)?.Options;

        public DbContextOptionsBuilder<T> GetDbContextOptionsBuilder(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return new DbContextOptionsBuilder<T>()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(warningsBuilder => warningsBuilder.Default(WarningBehavior.Log));
        }
    }
}
