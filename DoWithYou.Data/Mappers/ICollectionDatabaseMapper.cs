using DoWithYou.Data.Contexts;
using DoWithYou.Interface.Model;

namespace DoWithYou.Data.Mappers
{
    public interface ICollectionDatabaseMapper<T>
        where T : IModel
    {
        MongoDbContext MapCollectionToContext();
    }
}