using System;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;
using DomainModeling.EventRepository;

namespace DomainModeling.EventSourcing
{
    public class AggregateRepository<TAggregate> : IEventSourcedAggregateRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventDispatcher<TAggregate> _eventDispatcher;

        public AggregateRepository(IEventRepository eventRepository, IEventDispatcher<TAggregate> eventDispatcher)
        {
            _eventRepository = eventRepository;
            _eventDispatcher = eventDispatcher;
        }

        IAggregate<TAggregate> IAggregateRepository<TAggregate>.Instantiate(Guid id)
        {
            return Instantiate(id);
        }

        public IEventSourcedAggregate<TAggregate> Instantiate(Guid id)
        {
            return new UnrecordedAggregate<TAggregate>(id, this, _eventDispatcher);
        }

        async Task<IAggregate<TAggregate>> IAggregateRepository<TAggregate>.RetrieveAsync(Guid id)
        {
            return await RetrieveAsync(id);
        }

        public async Task<IEventSourcedAggregate<TAggregate>> RetrieveAsync(Guid id)
        {
            var eventList = await _eventRepository.RetriveEventsAsync<TAggregate>(id);
            var aggregate = new RecordedAggregate<TAggregate>(id, eventList.AggregateVersion, eventList.DomainEvents, this, _eventDispatcher);
            foreach (var @event in eventList.DomainEvents)
                await _eventDispatcher.DispatchAsync(aggregate.State, @event);
            return aggregate;
        }

        public async Task SaveUnrecordedAggregateAsync(UnrecordedAggregate<TAggregate> aggregate)
        {
            await _eventRepository.SaveNewStreamEventsAsync<TAggregate>(aggregate.State.Id, aggregate.UnrecordedEvents);
        }

        public async Task SaveRecordedAggregateAsync(RecordedAggregate<TAggregate> aggregate)
        {
            await _eventRepository.SaveEventsAsync<TAggregate>(aggregate.State.Id, aggregate.UnrecordedEvents, aggregate.Version);
        }
    }
}
