using System;
using System.Collections.Generic;
using System.Linq;
using Mc2.CrudTest.ModelFramework.Events;

namespace Mc2.CrudTest.ModelFramework.Entities
{
    public abstract class BaseAggregateRoot<TId> : BaseEntity<TId> where TId : IEquatable<TId>
    {
        private List<IEvent> _domainEvents;
        public Guid AggregateId { get; set; }

        public BaseAggregateRoot()
        {
            AggregateId = Guid.NewGuid();
        }

        public BaseAggregateRoot(IEnumerable<IEvent> events)
        {
            if (events == null) return;
            foreach (var @event in events)
                ((dynamic)this).On((dynamic)@event);
        }

        public IEnumerable<IEvent> GetEvents()
        {
            return _domainEvents.AsEnumerable();
        }

        protected void AddEvent(IEvent @event)
        {
            if (_domainEvents == null)
                _domainEvents = new List<IEvent>();
            _domainEvents.Add(@event);
        }

        protected void RemoveDomainEvent(IEvent @event)
        {
            _domainEvents.Remove(@event);
        }

        protected void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


        protected abstract void ValidateInvariants();


        public override bool Equals(object obj)
        {
            if (!(obj is BaseAggregateRoot<TId> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == null || other.Id == null)
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
