using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Command
{

    public class CommandResult : ApplicationServiceResult
    {

    }

    public class CommandResult<TData> : CommandResult
    {
        internal TData _data;
        public TData Data
        {
            get
            {
                return _data;
            }
        }

    }
}
