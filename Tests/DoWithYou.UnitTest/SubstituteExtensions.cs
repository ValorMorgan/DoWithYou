using System.Linq;
using DoWithYou.Data.Entities.DoWithYou.Base;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace DoWithYou.UnitTest
{
    internal static class SubstituteExtensions
    {
        internal static DbSet<T> Initialize<T>(this DbSet<T> dbSet, IQueryable<T> data)
            where T : BaseEntity
        {
            ((IQueryable<T>)dbSet).Provider.Returns(data.Provider);
            ((IQueryable<T>)dbSet).Expression.Returns(data.Expression);
            ((IQueryable<T>)dbSet).ElementType.Returns(data.ElementType);
            ((IQueryable<T>)dbSet).GetEnumerator().Returns(data.GetEnumerator());

            dbSet.AddRange(data);

            // Way to get Async support in EF 6.x. See: https://stackoverflow.com/a/21074664
            //if (dbSet is IDbAsyncEnumerable)
            //{
            //    ((IDbAsyncEnumerable<T>)dbSet).GetAsyncEnumerator()
            //        .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
            //    dbSet.Provider.Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            //}

            return dbSet;
        }
    }
}
