using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;
using Clearly.Core.Utilities.Interfaces;
using EventStore.ClientAPI;

namespace Clearly.EventRepository.EventStore
{
    public class EventStoreRepository : IEventRepository, IDisposable
    {
        private readonly IEventStoreConnection eventStore;
        private readonly IJsonByteConverter jsonByteConverter;
        private readonly EventStoreSettings settings;

        public EventStoreRepository(IEventStoreConnection eventStore, EventStoreSettings settings, IJsonByteConverter jsonByteConverter)
        {
            this.eventStore = eventStore;
            this.settings = settings;
            this.jsonByteConverter = jsonByteConverter;
        }

        public async Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents)
        {
            await SaveEventsAsync<T>(id, domainEvents, ExpectedVersion.NoStream);
        }

        public async Task SaveEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents, long aggregateVersion)
        {
            var stream = GetStream<T>(id);
            var data = domainEvents.Select(CreateEvent);
            await eventStore.AppendToStreamAsync(stream, aggregateVersion, data);
        }

        public async Task<AggregateEventList> RetriveEventsAsync<T>(Guid id)
        {
            var stream = GetStream<T>(id);
            //TODO: Handle this limit of 4000
            var data = await eventStore.ReadStreamEventsForwardAsync(stream, 0, 4000, false);

            return new AggregateEventList
            {
                AggregateVersion = data.LastEventNumber,
                DomainEvents = data.Events.Select(ConvertToDomainEvent)
            };
        }

        private IDomainEvent ConvertToDomainEvent(ResolvedEvent @event)
        {
            if (!settings.EventTypes.ContainsKey(@event.Event.EventType))
            {
                return null;
            }

            return jsonByteConverter.Deserialize(@event.Event.Data, settings.EventTypes[@event.Event.EventType]) as IDomainEvent;
        }

        private string GetStream<T>(Guid id)
        {
            var @namespace = settings.NamingPreferences.GetNamespaceName<T>();
            
            return $"{@namespace}-{id}";
        }

        private EventData CreateEvent(object data)
        {
            var eventType = settings.NamingPreferences.GetEventTypeName(data.GetType());
            var json = jsonByteConverter.Serialize(data);

            return new EventData(Guid.NewGuid(), eventType, true, json, new byte[0]);
        }

        public void Dispose()
        {
            eventStore?.Dispose();
        }
    }
}
