using System.Linq;
using SmartHome.DomainCore.Data;

namespace SmartHome.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> PageBy<TEntity>(this IQueryable<TEntity> query, PagingArgs? pagingArgs)
        {
            return pagingArgs == null ? query : query.Skip(pagingArgs.Skip).Take(pagingArgs.Take);
        }
    }
}