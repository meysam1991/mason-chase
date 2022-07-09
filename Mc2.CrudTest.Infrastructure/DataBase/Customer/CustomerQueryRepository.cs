using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.Infrastructure.BaseRepository;
using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.Shared.StringUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.DataBase.Customer
{
    public class CustomerQueryRepository : BaseEfQueryRepository<Mc2CrudTestDbContext,
           DomainModel.Customer.Entities.Customer, int>,
        ICustomerQueryRepository
    {
        public CustomerQueryRepository(Mc2CrudTestDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<CustomerItemDto> FindAsync(int id)
        {
            var findCustomer = await DbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (findCustomer != null)
            {
                return new CustomerItemDto
                {
                 BankAccountNumber=findCustomer.BankAccountNumber?.Value,
                 PhoneNumber=findCustomer.PhoneNumber?.Value,
                 CustomerId=findCustomer.Id,
                 DateOfBirth=findCustomer.DateOfBirth,
                 Email=findCustomer.Email?.Value,
                 FirstName=findCustomer?.FirstName.Value,
                 LastName=findCustomer?.LastName.Value,

                };
            }

            return null;
        }

        public async Task<Tuple<List<CustomerListItemDto>, int,int>> GetAllAsync(int page, int recordCount, string firstName, string LastName, DateTime? dateOfBirth, string phoneNumber, string email, string bankAccount)
        {
            var query =  DbContext.Customers.AsQueryable();
            var totalCount = query.Count();
            if (!firstName.IsNullOrEmpty())
                query = query.Where(x => x.FirstName.Value.Contains(firstName));

            if (!LastName.IsNullOrEmpty())
                query = query.Where(x => x.LastName.Value.Contains(LastName));

            if (!email.IsNullOrEmpty())
                query = query.Where(x => x.Email.Value.Contains(email));

            if (!phoneNumber.IsNullOrEmpty())
                query = query.Where(x => x.PhoneNumber.Value.Contains(phoneNumber));

            if (!bankAccount.IsNullOrEmpty())
                query = query.Where(x => x.BankAccountNumber.Value.Contains(bankAccount));

            if (dateOfBirth!=null)
                query = query.Where(x => x.DateOfBirth==dateOfBirth);

            var filteredCount = query.Count();

            var finalResult = await query.Select(customer => new CustomerListItemDto
            {
                 BankAccountNumber=customer.BankAccountNumber.Value,
                 FirstName=customer.FirstName.Value,
                 LastName=customer.LastName.Value,
                 Email=customer.Email.Value,
                 PhoneNumber=customer.PhoneNumber.Value,
                 CustomerId=customer.Id,
                 DateOfBirth=customer.DateOfBirth
            }).ToListAsync();

            return Tuple.Create(finalResult, totalCount, filteredCount);
        }
    }
}
