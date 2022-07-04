using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mc2.CrudTest.ModelFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mc2.CrudTest.Infrastructure.DataBase
{
    public static class EfDbContextAuditShadowProperties
    {
        public static readonly Func<object, int?> EFPropertyCreatedByUserId =
            entity => EF.Property<int?>(entity, CreatedByUserId);

        public static readonly string CreatedByUserId = nameof(CreatedByUserId);


        public static readonly Func<object, int?> EFPropertyModifiedByUserId =
            entity => EF.Property<int?>(entity, ModifiedByUserId);

        public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);


        public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime =
            entity => EF.Property<DateTime?>(entity, CreatedDateTime);

        public static readonly string CreatedDateTime = nameof(CreatedDateTime);


        public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime =
            entity => EF.Property<DateTime?>(entity, ModifiedDateTime);

        public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);


        public static readonly Func<object, string> EFPropertyCreatedByUserIp =
            entity => EF.Property<string>(entity, CreatedByUserIp);

        public static readonly string CreatedByUserIp = nameof(CreatedByUserIp);


        public static readonly Func<object, string> EFPropertyModifiedByUserIp =
            entity => EF.Property<string>(entity, ModifiedByUserIp);

        public static readonly string ModifiedByUserIp = nameof(ModifiedByUserIp);


        public static readonly Func<object, bool> EFPropertyIsDeleted =
            entity => EF.Property<bool>(entity, IsDeleted);

        public static readonly string IsDeleted = nameof(IsDeleted);

        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(CreatedByUserId);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(ModifiedByUserId);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>(CreatedDateTime);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>(ModifiedDateTime);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(CreatedByUserIp);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>(ModifiedByUserIp);
            }
        }

        public static void AddDeleteAbleShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => typeof(ISoftDeleteEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<bool?>(IsDeleted);
            }
        }

        public static void AddDeletebleQueryFilters(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(ISoftDeleteEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                Expression<Func<object, bool>> filter = e => EF.Property<bool>(e, "IsDeleted") != true
                    ;
                var body = filter.Body.ReplaceParameter(filter.Parameters[0], parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }


        public static Expression
            ReplaceParameter(this Expression expr, ParameterExpression source, Expression target) =>
            new ParameterReplacer { Source = source, Target = target }.Visit(expr);

        class ParameterReplacer : ExpressionVisitor
        {
            public ParameterExpression Source;
            public Expression Target;
            protected override Expression VisitParameter(ParameterExpression node) => node == Source ? Target : node;
        }

        public static void SetAuditableEntityPropertyValues(
            this ChangeTracker changeTracker
           )
        {
           
            var now = DateTime.UtcNow;
            
            var modifiedEntries = changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Deleted);
            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
                
            }

            var addedEntries = changeTracker.Entries<IAuditable>()
                .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Property(CreatedDateTime).CurrentValue = now;
                
            }
        }

        public static List<BaseAggregateRoot<TId>> GetChangedAggregates<TId>(this ChangeTracker changeTracker,
            bool getUnChangedAggregates = false)
            where TId : IEquatable<TId>
        {
            if (!getUnChangedAggregates)
                return changeTracker.Entries<BaseAggregateRoot<TId>>()
                    .Where(x => x.State == EntityState.Modified ||
                                x.State == EntityState.Added ||
                                x.State == EntityState.Deleted).Select(c => c.Entity).ToList();
            return changeTracker.Entries<BaseAggregateRoot<TId>>().Select(c => c.Entity).ToList();
        }

        public static List<BaseAggregateRoot<TId>> GetAllAggregates<TId>(this ChangeTracker changeTracker)
            where TId : IEquatable<TId> =>
            changeTracker.Entries<BaseAggregateRoot<TId>>()
                .Where(x => x.State != EntityState.Detached).Select(c => c.Entity).Where(c => c.GetEvents().Any())
                .ToList();
    }

    public class GenericSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }

    public class test
    {
    }
}