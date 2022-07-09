

using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.Queries;

namespace Mc2.CrudTest.ApplicationService.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomerQuery : PagedQuery, IQuery<CustomerListItemDto>
    {
        public GetAllCustomerQueryFilter GetAllCustomerQueryFilter { get; set; }
    }
}