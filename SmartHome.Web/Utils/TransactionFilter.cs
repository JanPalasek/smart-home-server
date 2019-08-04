using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartHome.Database;

namespace SmartHome.Web.Utils
{
    /// <summary>
    /// Handles beginning and ending of transaction for selected method.
    /// </summary>
    /// <remarks>
    /// Should be applied to controller through <see cref="Microsoft.AspNetCore.Mvc.ServiceFilterAttribute"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.TypeFilterAttribute"/>.
    /// </remarks>
    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly SmartHomeDbContext dbContext;

        public TransactionFilter(SmartHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // begin transaction before the method is invoked
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                // call next filter / method
                var actionExecutedContext = await next();

                // didn't return any exception => save and commit
                if (actionExecutedContext.Exception == null)
                {
                    await dbContext.SaveChangesAsync();

                    transaction.Commit();
                }

                // otherwise rollback (happens automatically)
            }
        }
    }
}