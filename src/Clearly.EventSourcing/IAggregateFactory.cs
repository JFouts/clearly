using System;
using System.Collections.Generic;
using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public interface IAggregateFactory<TAggregate> where TAggregate : AggregateRoot, new()
    {
        EventSourcedAggregate<TAggregate> CreateUnrecordedAggregate(Guid id);
        EventSourcedAggregate<TAggregate> CreateRecordedAggregate(Guid id, long version, IEnumerable<IDomainEvent> events);
    }
}