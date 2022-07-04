using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.Presentation.DomainModel.Customer.Dtos;

namespace Mc2.CrudTest.DomainModel.Customer.Data
{
    public interface ICustomerCommandRepository : ICommandRepository<Entities.Customer, int>
    {
        Task<bool> ExistCity(int cityId);
        Task<bool> HasAddressByDefaultAsync(string userId);
        Task<int> CountAddressAsync(int id);
        Task<List<CustomerItemListDto>> GetDefaultPhonesAsync(string userId);
        Task<IEnumerable<Entities.Customer>> GetDefaultAddressAsync(string userId);
    }
}
