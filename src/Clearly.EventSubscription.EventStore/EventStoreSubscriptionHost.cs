using System;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;
using Clearly.Core.Logging;
using Clearly.Core.Utilities.Interfaces;
using Clearly.EventRepository.EventStore;
using EventStore.ClientAPI;

namespace Clearly.EventSubscription.EventStore
{
    public class EventStoreSubscriptionHost : ISubscriptionHost, IDisposable
    {
        private readonly IEventStoreConnection _eventStore;
        private readonly IJsonByteConverter _jsonByteConverter;
        private readonly EventStoreSettings _eventStoreSettings;
        private readonly ILogger<EventStoreSubscriptionHost> _logger;

        public EventStoreSubscriptionHost(
            IEventStoreConnection eventStore,
            IJsonByteConverter jsonByteConverter,
            EventStoreSettings eventStoreSettings,
            ILogger<EventStoreSubscriptionHost> logger)
        {
            _eventStore = eventStore;
            _jsonByteConverter = jsonByteConverter;
            _eventStoreSettings = eventStoreSettings;
            _logger = logger;
        }

        public async Task StartAsync()
        {
            await _eventStore.ConnectAsync();
        }

        public void Subscribe(IEventStream stream, Func<IDomainEvent, Task> eventHandler)
        {
            var eventStoreStream = GetEventStoreStream(stream);

            switch (eventStoreStream.EventStreamType)
            {
                case EventStreamType.CatchUpSubscription:
                    SubscribeToCatchUp(eventStoreStream, async (_, @event) => await ProcessEvent(@event, eventHandler));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SubscribeToCatchUp(EventStoreEventStream stream, Func<EventStoreCatchUpSubscription, ResolvedEvent, Task> eventHandler)
        {
            _eventStore.SubscribeToStreamFrom(stream.StreamName, null, new CatchUpSubscriptionSettings(1, 1, false, true),
                eventHandler, CaughtUp, Dropped);
        }

        private void Dropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason reason, Exception exception)
        {
            if(exception == null)
                _logger?.LogWarning($"Subscription {subscription?.SubscriptionName} dropped for reason: {reason}");
            else
                _logger?.LogError(exception, $"Subscription {subscription?.SubscriptionName} dropped for reason: {reason}");
        }

        private void CaughtUp(EventStoreCatchUpSubscription subscription)
        {
            _logger?.LogInformation($"Subscription {subscription?.SubscriptionName} has caught up with live event stream.");
        }

        private async Task ProcessEvent(ResolvedEvent @event, Func<IDomainEvent, Task> eventHandler)
        {
            try
            {
                var domainEvent = ConvertToDomainEvent(@event);
                await eventHandler(domainEvent);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, $"Exception occured processing event {@event.OriginalEventNumber} from stream {@event.OriginalStreamId}");
            }
        }

        private IDomainEvent ConvertToDomainEvent(ResolvedEvent @event)
        {
            var eventType = _eventStoreSettings.EventTypes[@event.Event.EventType];

            return _jsonByteConverter.Deserialize(@event.Event.Data, eventType) as IDomainEvent;
        }

        private static EventStoreEventStream GetEventStoreStream(IEventStream stream)
        {
            if(!(stream is EventStoreEventStream eventStoreStream))
                throw new InvalidCastException();

            return eventStoreStream;
        }

        public void Dispose()
        {
            _eventStore?.Dispose();
        }
    }
}
