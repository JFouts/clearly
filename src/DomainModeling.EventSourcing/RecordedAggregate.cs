using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public class RecordedAggregate<TAggregate> : EventSourcedAggregate<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventSourcedAggregateRepository<TAggregate> _repository;

        public long Version { get; }
        public IEnumerable<IDomainEvent> Events { get; }

        public RecordedAggregate(Guid id, long version, IEnumerable<IDomainEvent> events, IEventSourcedAggregateRepository<TAggregate> repository, IEventDispatcher<TAggregate> eventDispatcher)
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
