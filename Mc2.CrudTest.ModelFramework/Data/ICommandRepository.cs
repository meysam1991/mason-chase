using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.Entities;

namespace Mc2.CrudTest.ModelFramework.Data
{
    public interface ICommandRepository<TEntity, in TKey> where TEntity : BaseAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> FindAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<bool> ExistAsync(TKey id);
        Task<bool> IsFieldValueUnique(Expression<Func<TEntity, bool>> checkFunction);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}