using Mc2.CrudTest.ModelFramework.Data;

namespace Mc2.CrudTest.DomainModel.Customer.Data
{
    public interface ICustomerCommandRepository : ICommandRepository<Entities.Customer, int>
    {
       
    }
}
