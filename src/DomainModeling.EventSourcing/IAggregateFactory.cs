using System;
using System.Collections.Generic;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public interface IAggregateFactory<TAggregate> where TAggregate : AggregateRoot, new()
    {
        EventSourcedAggregate<TAggregate> CreateUnrecordedAggregate(Guid id);
        EventSourcedAggregate<TAggregate> CreateRecordedAggregate(Guid id, long version, IEnumerable<IDomainEvent> events);
    }
}