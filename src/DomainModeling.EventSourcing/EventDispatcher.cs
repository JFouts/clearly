using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public class EventDispatcher<TAggregate> : IEventDispatcher<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly IEventHandlerFactory<TAggregate> _eventHandlerFactory;

        public EventDispatcher(IEventHandlerFactory<TAggregate> eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public async Task DispatchAsync(TAggregate aggregate, IDomainEvent @event)
        {
            var eventHandler = _eventHandlerFactory.CreateHandler(@event.GetType());
            await eventHandler.HandleAsync(aggregate, @event);
        }
    }
}
