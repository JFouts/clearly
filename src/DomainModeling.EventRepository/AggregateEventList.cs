using System.Collections.Generic;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventRepository
{
    public class AggregateEventList
    {
        public long AggregateVersion { get; set; }
        public IEnumerable<DomainEvent> DomainEvents { get; set; }
    }
}
