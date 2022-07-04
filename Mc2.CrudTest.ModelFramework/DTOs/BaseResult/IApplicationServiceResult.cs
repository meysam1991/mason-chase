using System.Collections.Generic;

namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public interface IApplicationServiceResult
    {
        IEnumerable<string> Messages { get; }
        ApplicationServiceStatus Status { get; set; }
    }
}