using Mc2.CrudTest.DomainModel.Customer.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.DomainModel.Customer.Data
{
    public interface ICustomerQueryRepository
    {
        Task<Tuple<List<CustomerListItemDto>, int,int>> GetAllAsync(int page, int recordCount,
              string firstName, string LastName, DateTime? dateOfBirth,
             string phoneNumber, string email, string bankAccount);
              
        Task<CustomerItemDto> FindAsync(int id);
    }
}
