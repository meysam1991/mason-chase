using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Infrastructure.DataBase.Events;
using Mc2.CrudTest.Infrastructure.DataBase.Events.OutBoxEventItemConfigurations;
using Mc2.CrudTest.ModelFramework.Entities;
using Mc2.CrudTest.ModelFramework.Events;
using Mc2.CrudTest.Shared.Serializations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using SirvanTspSwitch.Framework.Database.SqlDatabase.EntityFramework.Extensions;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Mc2.CrudTest.Infrastructure.DataBase.Common
{
    public class EfBaseMc2CrudTest : DbContext
    {
        private IDbContextTransaction _transaction;
        public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

        public EfBaseMc2CrudTest(DbContextOptions options)
            : base(options)
        {
        }

        protected EfBaseMc2CrudTest()
        {
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }

            _transaction.Rollback();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }

            _transaction.Commit();
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName).CurrentValue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAuditableShadowProperties();
            modelBuilder.AddDeleteAbleShadowProperties();
            modelBuilder.AddDeletebleQueryFilters();
            modelBuilder.ApplyConfiguration(new BaseOutBoxEventItemEfCoreConfig());


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.ValidateStrings();
            ChangeTracker.DetectChanges();
            BeforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ChangeTracker.ValidateStrings();
            ChangeTracker.DetectChanges();
            BeforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void BeforeSaveTriggers()
        {
            SetShadowProperties();
            AddOutboxEventItems();
        }

        private void AddOutboxEventItems()
        {
            var changedAggregatesInt = ChangeTracker.GetChangedAggregates<int>(true);
            var changedAggregatesLong = ChangeTracker.GetChangedAggregates<long>(true);
            var changedAggregatesShort = ChangeTracker.GetChangedAggregates<short>(true);
            var changedAggregatesString = ChangeTracker.GetChangedAggregates<string>(true);
            var serializer = this.GetService<IJsonSerializer>();
            foreach (var aggregate in changedAggregatesInt)
            {
                var events = aggregate.GetEvents();
                if (events == null)
                    continue;
                if (!events.Any())
                    continue;

                var keyName = Model.FindEntityType(aggregate.GetType()).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();
                var id = (int?)aggregate.GetType().GetProperty(keyName)?.GetValue(aggregate, null);
                var aggregateId = (Guid?)aggregate.GetType().GetProperty("AggregateId")?.GetValue(aggregate, null);

                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),

                        HappenedOn = DateTime.Now,
                        AggregateBusinessId = aggregateId,
                        AggregateId = id.ToString() ?? "0",
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayload = serializer.Serialize(@event),
                        IsProcessed = false,
                    });

                }
            }

            foreach (var aggregate in changedAggregatesLong)
            {
                var events = aggregate.GetEvents();
                if (events == null)
                    continue;
                if (!events.Any())
                    continue;

                var keyName = Model.FindEntityType(aggregate.GetType()).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();
                var id = (long?)aggregate.GetType().GetProperty(keyName)?.GetValue(aggregate, null);

                var aggregateId = (Guid?)aggregate.GetType().GetProperty("AggregateId")?.GetValue(aggregate, null);

                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),

                        HappenedOn = DateTime.Now,
                        AggregateBusinessId = aggregateId,
                        AggregateId = id.ToString() ?? "0",
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayload = serializer.Serialize(@event),
                        IsProcessed = false,
                    });
                }
            }

            foreach (var aggregate in changedAggregatesShort)
            {
                var events = aggregate.GetEvents();
                if (events == null)
                    continue;
                if (!events.Any())
                    continue;

                var keyName = Model.FindEntityType(aggregate.GetType()).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();
                var id = (short?)aggregate.GetType().GetProperty(keyName)?.GetValue(aggregate, null);

                var aggregateId = (Guid?)aggregate.GetType().GetProperty("AggregateId")?.GetValue(aggregate, null);
                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),

                        HappenedOn = DateTime.Now,
                        AggregateBusinessId = aggregateId,
                        AggregateId = id.ToString() ?? "0",
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayload = serializer.Serialize(@event),
                        IsProcessed = false,
                    });
                }
            }

            foreach (var aggregate in changedAggregatesString)
            {
                var events = aggregate.GetEvents();
                if (events == null)
                    continue;
                if (!events.Any())
                    continue;

                var keyName = Model.FindEntityType(aggregate.GetType()).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();
                var id = (string)aggregate.GetType().GetProperty(keyName)?.GetValue(aggregate, null);

                var aggregateId = (Guid?)aggregate.GetType().GetProperty("AggregateId")?.GetValue(aggregate, null);
                foreach (var @event in events)
                {
                    OutBoxEventItems.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),

                        HappenedOn = DateTime.Now,
                        AggregateBusinessId = aggregateId,
                        AggregateId = id.ToString() ?? "0",
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayload = serializer.Serialize(@event),
                        IsProcessed = false,
                    });
                }
            }
        }

        public void SetIsDeletedPropertyValue()
        {
            var serializer = this.GetService<IJsonSerializer>();

            var deletedEntries = ChangeTracker.Entries<ISoftDeleteEntity>()
                .Where(x => x.State == EntityState.Deleted);

            foreach (var deletedEntry in deletedEntries)
            {
                foreach (var deletedEntryReference in deletedEntry.References)
                {
                    deletedEntryReference.TargetEntry.State = EntityState.Unchanged;
                }
                deletedEntry.State = EntityState.Modified;
                deletedEntry.Property("IsDeleted").CurrentValue = true;
                var id = deletedEntry.Property("Id").CurrentValue;

                var @event = new SoftDelete();

                var deleteEvent = new OutBoxEventItem
                {
                    EventId = Guid.NewGuid(),

                    HappenedOn = DateTime.Now,
                    AggregateId = id.ToString() ?? "0",
                    AggregateName = deletedEntry.GetType().Name,
                    AggregateTypeName = deletedEntry.GetType().FullName,
                    EventName = @event.GetType().Name,
                    EventTypeName = @event.GetType().FullName,
                    EventPayload = serializer.Serialize(@event),
                    IsProcessed = false,

                };

                OutBoxEventItems.Add(deleteEvent);
            }
        }

        private void SetShadowProperties()
        {
            SetIsDeletedPropertyValue();
        }

        public IEnumerable<string> GetIncludePaths(Type clrEntityType)
        {
            var entityType = Model.FindEntityType(clrEntityType);
            var includedNavigation = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigation = entityType?.GetNavigations()
                    .Where(navigation => includedNavigation.Add(navigation)).ToList();
                if (entityNavigation?.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current?.Name));
                }
                else
                {
                    if (entityNavigation != null)
                    {
                        foreach (var navigation in entityNavigation)
                        {
                            var inverseNavigation = navigation.Inverse;
                            if (inverseNavigation != null)
                                includedNavigation.Add(inverseNavigation);
                        }

                        stack.Push(entityNavigation.GetEnumerator());
                    }
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0) break;
                entityType = stack.Peek().Current?.TargetEntityType;
            }
        }
    }
}