using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mc2.CrudTest.ModelFramework.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            var errors = context.ModelState.Where(x => x.Value.Errors.Any())
                .Select(kvp => string.Join(", ", kvp.Value.Errors.Select(p => p.ErrorMessage))).ToList();
            context.Result = new BadRequestObjectResult(errors);
        }
    }
}