using System.Linq;
using SmartHome.DomainCore.Data;

namespace SmartHome.Shell.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> query, PagingArgs pagingArgs)
        {
            return query.Skip(pagingArgs.Skip).Take(pagingArgs.Take);
        }
    }
}