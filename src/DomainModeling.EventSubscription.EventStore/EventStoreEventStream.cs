namespace DomainModeling.EventSubscription.EventStore
{
    public class EventStoreEventStream : IEventStream
    {
        public string StreamName { get; set; } = string.Empty;

        public EventStreamType EventStreamType { get; set; } = EventStreamType.CatchUpSubscription;
    }
}