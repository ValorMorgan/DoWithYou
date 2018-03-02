using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Factories
{
    public interface IDbContextOptionsFactory<T> where T : DbContext
    {
        DbContextOptionsBuilder<T> GetDbContextOptionsBuilder(string connectionString);
        DbContextOptions<T> GetOptions(string connectionString);
    }
}