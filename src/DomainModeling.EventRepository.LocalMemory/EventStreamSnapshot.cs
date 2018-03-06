using System.Collections.Generic;

namespace DomainModeling.EventRepository.LocalMemory
{
    public class EventStreamSnapshot
    {
        public long Version { get; }
        public IEnumerable<object> Events { get; }

        public EventStreamSnapshot(long version, IEnumerable<object> events)
        {
            Version = version;
            Events = events;
        }
    }
}