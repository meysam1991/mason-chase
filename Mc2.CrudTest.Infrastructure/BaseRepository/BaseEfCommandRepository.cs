using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mc2.CrudTest.Infrastructure.DataBase.Common;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.Entities;
using Microsoft.EntityFrameworkCore;


namespace Mc2.CrudTest.Infrastructure.BaseRepository
{
    public class BaseEfCommandRepository<TEntity, TKey, TDbContext> : ICommandRepository<TEntity, TKey>
        where TEntity : BaseAggregateRoot<TKey> where TKey : IEquatable<TKey> where TDbContext : EfBaseMc2CrudTest
    {
        protected readonly TDbContext Context;

        public BaseEfCommandRepository(TDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<bool> ExistAsync(TKey id)
        {
            return await Context.Set<TEntity>().AnyAsync(x => x.Id.Equals(id));
        }



        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<bool> IsFieldValueUnique(Expression<Func<TEntity, bool>> checkFunction)
        {
            try
            {
                var item = await Context.Set<TEntity>().SingleOrDefaultAsync(checkFunction);
                return item == null;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}