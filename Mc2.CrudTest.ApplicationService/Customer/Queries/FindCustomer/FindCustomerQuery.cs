

using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.Queries;

namespace Mc2.CrudTest.ApplicationService.Customer.Queries.FindCustomer
{
    public class FindCustomerQuery : IQuery<CustomerItemDto>
    {
        public int CustomerId { get; set; }
    }
}