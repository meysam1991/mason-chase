using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Mc2.CrudTest.ModelFramework.Logger;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.ModelFramework.Filters
{
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackActionPerformanceFilter> _logger;
        private readonly IScopeInformation _scopeInfo;
        private IDisposable _userScope;
        private IDisposable _hostScope;
        private IDisposable _requestScope;

        public TrackActionPerformanceFilter(
            ILogger<TrackActionPerformanceFilter> logger,
            IScopeInformation scopeInfo)
        {
            _logger = logger;
            _scopeInfo = scopeInfo;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();
            var state = new Dictionary<string, string>
            {
                {
                    "UserId", context.HttpContext.User.Claims
                        .FirstOrDefault( a => a.Type == "sub")
                        ?.Value
                }
            };
            var source = context.HttpContext.User.Claims.Where(c => c.Type == "scope");
            state.Add("OAuth2 Scopes", string.Join(",", source.Select(c => c.Value)));
            _userScope = _logger.BeginScope(state);
            _hostScope = _logger.BeginScope(_scopeInfo.HostScopeInfo);
            _requestScope = _logger.BeginScope(_scopeInfo.RequestScopeInfo);
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if (context.Exception == null)
                _logger.Log(LogLevel.Information, 0,
                    $"{context.HttpContext.Request.Path} {context.HttpContext.Request.Method} " +
                    $"code took {_timer.ElapsedMilliseconds} ms.");
            _userScope?.Dispose();
            _hostScope?.Dispose();
            _requestScope.Dispose();
        }
    }
}