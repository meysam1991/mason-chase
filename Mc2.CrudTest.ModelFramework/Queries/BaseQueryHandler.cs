using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Queries
{
    public abstract class BaseQueryHandler<TQuery, TResult, TData> : IQueryHandler<TQuery, TResult, TData>
        where TQuery : class, IQuery<TData> where TResult : BaseResult
    {
        public abstract Task<TResult> HandleAsync(TQuery request);
    }
}