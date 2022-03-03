using System;
using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public interface IDomainEventRegistry<out TAggregate> where TAggregate : AggregateRoot
    {
        Type GetHandlerType(Type eventType);

        void RegisterHandler<TEvent, THandler>()
            where TEvent : IDomainEvent
            where THandler : IDomainEventHandler<TAggregate, TEvent>;
    }
}
