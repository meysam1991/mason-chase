using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationService.Customer.Queries.GetAllCustomers
{
    public class GetAllCustomerQueryHandler : IQueryHandler<GetAllCustomerQuery, PagedResult<CustomerListItemDto>,
        CustomerListItemDto>
    {
        private readonly ICustomerQueryRepository _queryRepository;
       

        public GetAllCustomerQueryHandler(ICustomerQueryRepository queryRepository )
          
        {
            _queryRepository = queryRepository;
             
        }


        public async Task<PagedResult<CustomerListItemDto>> HandleAsync(GetAllCustomerQuery request)
        {
            var counter = 1;
            if (request == null)
                return new PagedResult<CustomerListItemDto>(new Error(ErrorCode.EmptyData,
                  "Empty request",
                    nameof(request)));

            var (data, filterCount, totalCount) = await _queryRepository.GetAllAsync(
                request.Page,
                request.RecordCount,
                request?.GetAllCustomerQueryFilter?.FirstName ?? null,
                request?.GetAllCustomerQueryFilter?.LastName ?? null,
                request?.GetAllCustomerQueryFilter?.DateOfBirth ?? null,
                request?.GetAllCustomerQueryFilter?.PhoneNumber ?? null,
                request?.GetAllCustomerQueryFilter?.Email ?? null,
                request?.GetAllCustomerQueryFilter?.BankAccount ?? null
            );

            data.ForEach(x => x.Counter = counter++);
            return new PagedResult<CustomerListItemDto>(data, request.Page, totalCount, filterCount);
        }
    }
}