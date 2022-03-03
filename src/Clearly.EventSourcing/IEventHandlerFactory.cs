using System;

namespace Clearly.EventSourcing
{
    public interface IEventHandlerFactory<TAggregate>
    {
        IDomainEventHandler<TAggregate> CreateHandler(Type eventType);
    }
}
