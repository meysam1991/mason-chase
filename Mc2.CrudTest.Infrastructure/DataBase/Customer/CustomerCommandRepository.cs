using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.DataBase.Customer
{
    public class CustomerCommandRepository :
        BaseEfCommandRepository<Mc2.CrudTest.DomainModel.Customer.Entities.Customer, int, Mc2CrudTestDbContext>,
        ICustomerCommandRepository
    {
        public CustomerCommandRepository(Mc2CrudTestDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<DomainModel.Customer.Entities.Customer> FindCustomerById(int customerId)
        {
           return await Context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
        }
    }
}