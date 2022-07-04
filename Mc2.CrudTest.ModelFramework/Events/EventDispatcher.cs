using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Events
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;
        public EventDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceFactory = serviceScopeFactory;
        }
        #region Event Dispatcher
        public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IEvent
        {
            using var serviceProviderScope = _serviceFactory.CreateScope();
            var handlers = serviceProviderScope.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }

        public async Task PublishDomainEventAsync(Parcel parcel)
        {
            using var serviceProviderScope = _serviceFactory.CreateScope();
            var handlers = serviceProviderScope.ServiceProvider.GetRequiredService<IDomainEventHandler>();
            await handlers.Handle(parcel);
        }

        #endregion

    }
}
