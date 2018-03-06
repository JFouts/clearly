using System.Collections.Generic;
using System.Data;

namespace DomainModeling.EventRepository.LocalMemory
{
    public class EventStream
    {
        private long _version;
        private readonly List<object> _events;

        public EventStream(IEnumerable<object> events)
        {
            _events = new List<object>(events);
            _version = 1;
        }

        public void AppendEvents(long expectedVersion, IEnumerable<object> events)
        {
            lock (_events)
                AppendEventsAtomic(expectedVersion, events);
        }

        public EventStreamSnapshot RetrieveSnapshot()
        {
            lock (_events)
                return new EventStreamSnapshot(_version, _events.ToArray());
        }

        private void AppendEventsAtomic(long expectedVersion, IEnumerable<object> events)
        {
            AssertVersion(expectedVersion);
            _events.AddRange(events);
            _version++;
        }

        private void AssertVersion(long expectedVersion)
        {
            if (expectedVersion != _version)
                throw new DBConcurrencyException("Expected version did not match");
        }
    }
}