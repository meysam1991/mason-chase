using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> Execute<TQuery, TResult, TData>(TQuery query)
            where TQuery : class, IQuery<TData> where TResult : BaseResult;

        Task<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : class, IQuery<TResult> where TResult : BaseResult;
    }
}
