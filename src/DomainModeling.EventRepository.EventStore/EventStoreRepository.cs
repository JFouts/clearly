using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Utilities.Interfaces;
using EventStore.ClientAPI;

namespace DomainModeling.EventRepository.EventStore
{
    public class EventStoreRepository : IEventRepository, IDisposable
    {
        private readonly IEventStoreConnection _eventStore;
        private readonly IJsonByteConverter _jsonByteConverter;
        private readonly EventStoreSettings _settings;

        public EventStoreRepository(IEventStoreConnection eventStore, EventStoreSettings settings, IJsonByteConverter jsonByteConverter)
        {
            _eventStore = eventStore;
            _settings = settings;
            _jsonByteConverter = jsonByteConverter;
        }

        public async Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents)
        {
            await SaveEventsAsync<T>(id, domainEvents, ExpectedVersion.NoStream);
        }

        public async Task SaveEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents, long aggregateVersion)
        {
            var stream = GetStream<T>(id);
            var data = domainEvents.Select(CreateEvent);
            await _eventStore.AppendToStreamAsync(stream, aggregateVersion, data);
        }

        public async Task<AggregateEventList> RetriveEventsAsync<T>(Guid id)
        {
            var stream = GetStream<T>(id);
            //TODO: Handle this limit of 4000
            var data = await _eventStore.ReadStreamEventsForwardAsync(stream, 0, 4000, false);

            return new AggregateEventList
            {
                AggregateVersion = data.LastEventNumber,
                DomainEvents = data.Events.Select(ConvertToDomainEvent)
            };
        }

        private DomainEvent ConvertToDomainEvent(ResolvedEvent @event)
        {
            if (!_settings.EventTypes.ContainsKey(@event.Event.EventType))
                return null;

            return _jsonByteConverter.Deserialize(@event.Event.Data, _settings.EventTypes[@event.Event.EventType]) as DomainEvent;
        }

        private string GetStream<T>(Guid id)
        {
            var @namespace = _settings.NamingPreferences.GetNamespaceName<T>();
            return $"{@namespace}-{id}";
        }

        private EventData CreateEvent(object data)
        {
            var eventType = _settings.NamingPreferences.GetEventTypeName(data.GetType());
            var json = _jsonByteConverter.Serialize(data);
            return new EventData(Guid.NewGuid(), eventType, true, json, new byte[0]);
        }

        public void Dispose()
        {
            _eventStore?.Dispose();
        }
    }
}
