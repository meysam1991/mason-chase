using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ModelFramework.Queries
{

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceFactory = serviceScopeFactory;
        }

        #region Query Dispatcher

        public async Task<TResult> Execute<TQuery, TResult, TData>(TQuery query) where TQuery : class, IQuery<TData>
            where TResult : BaseResult
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult, TData>>();
            return await handler.HandleAsync(query);
        }

        public async Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult> where TResult : BaseResult
        {
            using var serviceScope = _serviceFactory.CreateScope();
            var handler = serviceScope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }

        #endregion
    }
}
