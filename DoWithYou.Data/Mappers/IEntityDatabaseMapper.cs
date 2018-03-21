using DoWithYou.Interface.Entity.SQL;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Mappers
{
    public interface IEntityDatabaseMapper<T>
        where T : IBaseEntity
    {
        DbContext MapEntityToContext();
    }
}