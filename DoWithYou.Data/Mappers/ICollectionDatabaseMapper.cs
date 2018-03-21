using DoWithYou.Data.Contexts;
using DoWithYou.Interface.Entity.NoSQL;

namespace DoWithYou.Data.Mappers
{
    public interface ICollectionDatabaseMapper<T>
        where T : IBaseDocument
    {
        MongoDbContext MapCollectionToContext();
    }
}