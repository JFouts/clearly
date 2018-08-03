using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public abstract class DomainEventHandler<TAggregate, TEvent> : IDomainEventHandler<TAggregate, TEvent> where TEvent : DomainEvent
    {
        public abstract Task HandleAsync(TAggregate aggregate, TEvent @event);

        async Task IDomainEventHandler<TAggregate>.HandleAsync(TAggregate aggregate, DomainEvent @event)
        {
            if (@event.GetType() != typeof(TEvent))
                throw new InvalidDomainEventTypeException();

            await HandleAsync(aggregate, (TEvent)@event);
        }
    }
}
