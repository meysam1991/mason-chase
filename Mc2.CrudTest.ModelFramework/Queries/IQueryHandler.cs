using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Queries
{
    public interface IQueryHandler<in TQuery, TResult, TData>
        where TQuery : class, IQuery<TData> where TResult : BaseResult
    {
        Task<TResult> HandleAsync(TQuery request);
    }

    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : class, IQuery<TResult> where TResult : BaseResult
    {
        Task<TResult> HandleAsync(TQuery request);
    }
}