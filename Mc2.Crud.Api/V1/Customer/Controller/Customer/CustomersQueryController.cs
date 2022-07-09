using System.Threading.Tasks;
using Mc2.CrudTest.ApplicationService.Customer.Queries.FindCustomer;
using Mc2.CrudTest.ApplicationService.Customer.Queries.GetAllCustomers;
using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Queries;
using Mc2.CrudTest.Shared.CustomAnnotation;
using Mc2.CrudTest.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
 

namespace Mc2.Crud.Api.V1.Customer.Controller.Customer
{
    [ApiController]
    [ControllerDescription("Customer")]
    public class CustomersQueryController : ControllerBase
    {
        private IQueryDispatcher QueryDispatcher => HttpContext.QueryDispatcher();


        [HttpGet("FindCustomer")]
        [MapToApiVersion("1.0")]
        [ActionDescription("FindCustomer")]
        public async Task<Result<CustomerItemDto>> FindCustomer([FromQuery] FindCustomerQuery query)
        {
            return await QueryDispatcher
                .Execute<FindCustomerQuery, Result<CustomerItemDto>, CustomerItemDto>(query);
        }

        [HttpGet("GetAllCustomers")]
        [MapToApiVersion("1.0")]
        public async Task<PagedResult<CustomerListItemDto>> GetAllCustomers([FromQuery] GetAllCustomerQuery query)
        {
            return await QueryDispatcher
                .Execute<GetAllCustomerQuery, PagedResult<CustomerListItemDto>, CustomerListItemDto>(query);
        }

    }
}
