using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing.AspNetCore
{
    public interface IAggregateBuilder<in TAggregate> where TAggregate : AggregateRoot
    {
        IAggregateBuilder<TAggregate> AddEvent<TEvent>() where TEvent : IDomainEvent;
    }
}