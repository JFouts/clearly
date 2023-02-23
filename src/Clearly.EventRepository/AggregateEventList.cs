using Clearly.Core.Interfaces;

namespace Clearly.EventRepository;

public class AggregateEventList
{
    public long AggregateVersion { get; set; }
    public IEnumerable<IDomainEvent> DomainEvents { get; set; } = Array.Empty<IDomainEvent>();
}
