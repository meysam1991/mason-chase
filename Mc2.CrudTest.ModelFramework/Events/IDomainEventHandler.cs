using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Events
{
    public interface IDomainEventHandler<in TDomainEvent>
        where TDomainEvent : IEvent
    {
        Task Handle(TDomainEvent Event);
    }

    public interface IDomainEventHandler
    {
        Task Handle(Parcel parcel);
    }
}
