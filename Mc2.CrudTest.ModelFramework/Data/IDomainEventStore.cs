using System.Collections.Generic;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.Events;

namespace Mc2.CrudTest.ModelFramework.Data
{
    public interface IDomainEventStore
    {
        void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IEvent;
        Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IEvent;
    }
}
