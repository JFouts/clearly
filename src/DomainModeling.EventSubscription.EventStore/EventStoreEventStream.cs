namespace DomainModeling.EventSubscription.EventStore
{
    public class EventStoreEventStream : IEventStream
    {
        public string StreamName { get; set; }

        public EventStreamType EventStreamType { get; set; } = EventStreamType.CatchUpSubscription;
    }
}