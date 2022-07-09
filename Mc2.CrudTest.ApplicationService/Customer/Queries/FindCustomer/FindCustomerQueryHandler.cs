using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Queries;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationService.Customer.Queries.FindCustomer
{
    public class FindCustomerQueryHandler : IQueryHandler<FindCustomerQuery, Result<CustomerItemDto>, CustomerItemDto>
    {
        private readonly ICustomerQueryRepository _repository;
     

        public FindCustomerQueryHandler(ICustomerQueryRepository repository
           )
        {
            _repository = repository;
            
        }

        public async Task<Result<CustomerItemDto>> HandleAsync(FindCustomerQuery request)
        {
            if (request == null)
                return new Result<CustomerItemDto>(new Error(ErrorCode.EmptyData,
                   "Empty Request"));


            var find = await _repository.FindAsync(request.CustomerId);
            if (find == null)
                return new Result<CustomerItemDto>(new Error(ErrorCode.NotFound,
                 "Customer Not Found",nameof(request.CustomerId)));

            return new Result<CustomerItemDto>(find);


        }
    }
}