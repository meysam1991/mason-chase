using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Events
{
    public interface IEventDispatcher
    {
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IEvent;
        Task PublishDomainEventAsync(Parcel parcel);
    }
}
