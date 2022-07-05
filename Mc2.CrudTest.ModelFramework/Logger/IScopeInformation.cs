using System.Collections.Generic;

namespace Mc2.CrudTest.ModelFramework.Logger
{
    public interface IScopeInformation
    {
        Dictionary<string, string> HostScopeInfo { get; }
        Dictionary<string, string> RequestScopeInfo { get; }
    }
}
