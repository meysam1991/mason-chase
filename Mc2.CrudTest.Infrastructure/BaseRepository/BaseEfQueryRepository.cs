using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.Entities;
using System;

namespace Mc2.CrudTest.Infrastructure.BaseRepository
{
    public class BaseEfQueryRepository<TDbContext, TEntity, TKey> : IQueryRepository
        where TDbContext : EfBaseMc2CrudTest where TEntity : BaseAggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        protected readonly TDbContext DbContext;

        public BaseEfQueryRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}