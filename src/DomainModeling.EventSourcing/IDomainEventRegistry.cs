using System;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public interface IDomainEventRegistry<out TAggregate> where TAggregate : AggregateRoot
    {
        Type GetHandlerType(Type eventType);

        void RegisterHandler<TEvent, THandler>()
            where TEvent : IDomainEvent
            where THandler : IDomainEventHandler<TAggregate, TEvent>;
    }
}
