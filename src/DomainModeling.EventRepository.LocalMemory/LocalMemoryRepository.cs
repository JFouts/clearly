using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventRepository.LocalMemory
{
    public class LocalMemoryRepository : IEventRepository
    {
        private readonly ConcurrentDictionary<Guid, EventStream> _eventStreams = new ConcurrentDictionary<Guid, EventStream>();

        public Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents)
        {
            SaveNewStreamEvents(id, domainEvents);
            return Task.CompletedTask;
        }

        public Task SaveEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents, long aggregateVersion)
        {
            SaveEvents(id, domainEvents, aggregateVersion);
            return Task.CompletedTask;
        }

        public Task<AggregateEventList> RetriveEventsAsync<T>(Guid id)
        {
            return Task.FromResult(RetriveEvents(id));
        }

        private void SaveNewStreamEvents(Guid id, IEnumerable<DomainEvent> domainEvents)
        {
            _eventStreams.TryAdd(id, new EventStream(domainEvents));
        }

        private void SaveEvents(Guid id, IEnumerable<DomainEvent> domainEvents, long aggregateVersion)
        {
            if (_eventStreams.TryGetValue(id, out var value))
                value.AppendEvents(aggregateVersion, domainEvents);
        }

        private AggregateEventList RetriveEvents(Guid id)
        {
            if (!_eventStreams.TryGetValue(id, out var stream))
                throw new KeyNotFoundException();

            return RetrieveEventList(stream);
        }

        private static AggregateEventList RetrieveEventList(EventStream stream)
        {
            var snapshot = stream.RetrieveSnapshot();
            return new AggregateEventList
            {
                AggregateVersion = snapshot.Version,
                DomainEvents = snapshot.Events.Select(x => x as DomainEvent)
            };
        }
    }
}
