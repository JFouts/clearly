using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public class RecordedAggregate<TAggregate> : EventSourcedAggregate<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventSourcedAggregateRepository<TAggregate> _repository;

        public long Version { get; }
        public IEnumerable<DomainEvent> Events { get; }

        public RecordedAggregate(Guid id, long version, IEnumerable<DomainEvent> events, IEventSourcedAggregateRepository<TAggregate> repository, IEventDispatcher<TAggregate> eventDispatcher)
            : base(id, eventDispatcher)
        {
            Version = version;
            Events = events;
            _repository = repository;
        }

        public override async Task SaveAsync()
        {
            await _repository.SaveRecordedAggregateAsync(this);
        }
    }
}
