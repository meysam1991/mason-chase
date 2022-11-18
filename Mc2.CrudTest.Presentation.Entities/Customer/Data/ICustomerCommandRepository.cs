using Mc2.CrudTest.ModelFramework.Data;
using System.Threading.Tasks;

namespace Mc2.CrudTest.DomainModel.Customer.Data
{
    public interface ICustomerCommandRepository : ICommandRepository<Entities.Customer, int>
    {
      Task<Entities.Customer> FindCustomerById(int customerId);
       
    }
}
