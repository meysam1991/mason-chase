using Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.ApplicationService.Customer.Commands.DeleteCustomer;
using Mc2.CrudTest.ApplicationService.Customer.Commands.UpdateCustomer;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.Shared.CustomAnnotation;
using Mc2.CrudTest.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mc2.Crud.Api.V1.Customer.Controller
{
    [ApiController]
    [ControllerDescription("Customer")]
    public class CustomerCommandController : ControllerBase
    {
        private ICommandDispatcher CommandDispatcher => HttpContext.CommandDispastcher();

        [HttpPost("CreateCustomer")]
        [ActionDescription("CreateCustomer")]
        public async Task<BaseResult> CreateCustomer(CreateCustomerCommand command)
        {
            return await CommandDispatcher
                .SendAsync<CreateCustomerCommand>(command);
        }

        [HttpPost("UpdateCustomer")]
        [ActionDescription("UpdateCustomer")]
        public async Task<BaseResult> UpdateCustomer(UpdateCustomerCommand command)
        {
            return await CommandDispatcher
                .SendAsync<UpdateCustomerCommand>(command);
        }

        [HttpPost("DeleteCustomer")]
        [ActionDescription("DeleteCustomer")]
        public async Task<BaseResult> UpdateCustomer(DeleteCustomerCommand command)
        {
            return await CommandDispatcher
                .SendAsync<DeleteCustomerCommand>(command);
        }
    }
}
