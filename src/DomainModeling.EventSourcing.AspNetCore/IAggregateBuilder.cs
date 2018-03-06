using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing.AspNetCore
{
    public interface IAggregateBuilder<in TAggregate> where TAggregate : AggregateRoot
    {
        IAggregateBuilder<TAggregate> AddEvent<TEvent>() where TEvent : IDomainEvent;
    }
}