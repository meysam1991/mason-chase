using System;
using System.Data;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.Entities;

namespace Mc2.CrudTest.ModelFramework.Data
{
    public interface IUnitOfWorkDapper : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        Task<int> CommitAsync();
        IDbTransaction BeginTransaction();
        void Rollback();
        void SetAuditValues(IAuditable entity);
        Task StoreEventInOutBoxAsync<TId>(BaseAggregateRoot<TId> aggregate) where TId : IEquatable<TId>;
    }
}