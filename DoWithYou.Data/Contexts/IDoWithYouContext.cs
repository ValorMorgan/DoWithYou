using System;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data.Contexts
{
    public interface IDoWithYouContext : IDisposable
    {
        int SaveChanges();

        DbSet<T> Set<T>()
            where T : class;
    }
}