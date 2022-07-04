using System.Data;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Data
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        Task<int> CommitAsync();
        IDbTransaction BeginTransaction();
      
    }
}