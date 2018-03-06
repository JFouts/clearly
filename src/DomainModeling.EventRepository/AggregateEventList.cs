using System.Collections.Generic;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventRepository
{
    public class AggregateEventList
    {
        public long AggregateVersion { get; set; }
        public IEnumerable<IDomainEvent> DomainEvents { get; set; }
    }
}
