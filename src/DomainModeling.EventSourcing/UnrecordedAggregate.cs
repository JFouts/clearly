using System;
using System.Threading.Tasks;
using DomainModeling.Core;

namespace DomainModeling.EventSourcing
{
    public class UnrecordedAggregate<TAggregate> : EventSourcedAggregate<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventSourcedAggregateRepository<TAggregate> _repository;

        public UnrecordedAggregate(Guid id, IEventSourcedAggregateRepository<TAggregate> repository, IEventDispatcher<TAggregate> eventDispatcher)
            : base(id, eventDispatcher)
        {
            _repository = repository;
        }

        public override async Task SaveAsync()
        {
            await _repository.SaveUnrecordedAggregateAsync(this);
        }
    }
}
