
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Queries
{
    public sealed class QueryResult<TData> : ApplicationServiceResult
    {
        private readonly TData _data;

        public QueryResult(TData data)
        {
            _data = data;
        }

        public TData Data
        {
            get
            {
                return _data;
            }
        }
    }
}
