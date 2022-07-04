using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Queries;
using Microsoft.AspNetCore.Http;
 

namespace Mc2.CrudTest.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static ICommandDispatcher CommandDispastcher(this HttpContext httpContext) =>
            (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
    }
}