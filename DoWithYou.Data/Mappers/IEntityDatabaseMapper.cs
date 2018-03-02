using DoWithYou.Interface.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Mappers
{
    public interface IEntityDatabaseMapper<T>
        where T : IBaseEntity
    {
        DbContext MapEntityToContext();
    }
}