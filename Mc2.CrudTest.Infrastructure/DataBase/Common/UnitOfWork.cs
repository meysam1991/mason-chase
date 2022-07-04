using Mc2.CrudTest.ModelFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.DataBase.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Mc2CrudTestDbContext _context;

        public UnitOfWork(Mc2CrudTestDbContext context)
        {
            _context = context;
            Connection = _context.Database.GetDbConnection();
        }

        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IDbTransaction BeginTransaction()
        {
            var tr = _context.Database.BeginTransaction();
            return tr.GetDbTransaction();
        }

         
    }
}