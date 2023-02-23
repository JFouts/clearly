using System.Collections.Concurrent;
using Clearly.Core.Interfaces;

namespace Clearly.EventRepository.LocalMemory
{
    public class LocalMemoryRepository : IEventRepository
    {
        private readonly ConcurrentDictionary<Guid, EventStream> _eventStreams = new ConcurrentDictionary<Guid, EventStream>();

        public Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents)
        {
            SaveNewStreamEvents(id, domainEvents);
            return Task.CompletedTask;
        }

        public Task SaveEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents, long aggregateVersion)
        {
            SaveEvents(id, domainEvents, aggregateVersion);
            return Task.CompletedTask;
        }

        public Task<AggregateEventList> RetrieveEventsAsync<T>(Guid id)
        {
            return Task.FromResult(RetrieveEvents(id));
        }

        private void SaveNewStreamEvents(Guid id, IEnumerable<IDomainEvent> domainEvents)
        {
            _eventStreams.TryAdd(id, new EventStream(domainEvents));
        }

        private void SaveEvents(Guid id, IEnumerable<IDomainEvent> domainEvents, long aggregateVersion)
        {
            if (_eventStreams.TryGetValue(id, out var value))
            {
                value.AppendEvents(aggregateVersion, domainEvents);
            }
        }

        private AggregateEventList RetrieveEvents(Guid id)
        {
            if (!_eventStreams.TryGetValue(id, out var stream))
            {
                throw new KeyNotFoundException();
            }

            return RetrieveEventList(stream);
        }

        private static AggregateEventList RetrieveEventList(EventStream stream)
        {
            var snapshot = stream.RetrieveSnapshot();

            return new AggregateEventList
            {
                AggregateVersion = snapshot.Version,
                DomainEvents = snapshot.Events.OfType<IDomainEvent>()
            };
        }
    }
}
