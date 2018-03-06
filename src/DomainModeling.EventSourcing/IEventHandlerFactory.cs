using System;

namespace DomainModeling.EventSourcing
{
    public interface IEventHandlerFactory<TAggregate>
    {
        IDomainEventHandler<TAggregate> CreateHandler(Type eventType);
    }
}
