using Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.Shared.CustomAnnotation;
using Mc2.CrudTest.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MC2.API.Controllers
{
    [ApiController]
    [ControllerDescription("عملیات مربوط به آدرس")]

    public class CustomerCommandController : ControllerBase
    {
        private ICommandDispatcher CommandDispatcher => HttpContext.CommandDispastcher();

        [HttpPost("CreateCustomer")]
        [MapToApiVersion("1.0")]
        [ActionDescription("CreateCustomer")]
        public async Task<BaseResult> CreateCustomer(CreateCustomerCommand command)
        {
            return await CommandDispatcher
                .SendAsync<CreateCustomerCommand>(command);
        }


    }
}
