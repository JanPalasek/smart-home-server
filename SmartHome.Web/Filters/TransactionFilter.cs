namespace SmartHome.Web.Filters
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Handles beginning and ending of transaction for selected method.
    /// </summary>
    /// <remarks>
    /// Should be applied to controller through <see cref="ServiceFilterAttribute"/>
    /// or <see cref="TypeFilterAttribute"/>.
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