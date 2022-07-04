using Mc2.CrudTest.DomainModel.Customer.Dtos;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.Presentation.DomainModel.Customer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mc2.CrudTest.DomainModel.Customer.Data
{
    public interface ICustomerQueryRepository : IQueryRepository
    {
        Task<List<CustomerItemListDto>> GetAllAsync(int page, int recordCount, string userId, string cityName,
            string provinceName, string title, string addressLine, string postalCode);
        Task<int> GetAllCountAsync(string userId, string cityName, string provinceName, string title,
            string addressLine, string postalCode);
        Task<CustomerDto> FindAsync(int id);
        Task<int> GetCountAsync(string userId);
    }
}
