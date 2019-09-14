using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CeleryArchitectureDemo.Infrastructure
{
    public class TodoContextTransactionFilter : IAsyncActionFilter
    {
        private readonly TodoContext _database;

        public TodoContextTransactionFilter(TodoContext _database)
        {
            this._database = _database;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                _database.BeginTransaction();

                await next();

                await _database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                _database.RollbackTransaction();
                throw;
            }
        }
    }
}