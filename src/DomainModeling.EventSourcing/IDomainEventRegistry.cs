using System;
using DomainModeling.Core;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public interface IDomainEventRegistry<out TAggregate> where TAggregate : AggregateRoot
    {
        Type GetHandlerType(Type eventType);

        void RegisterHandler<TEvent, THandler>()
            where TEvent : DomainEvent
            where THandler : IDomainEventHandler<TAggregate, TEvent>;
    }
}
