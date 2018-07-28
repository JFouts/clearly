using System;

namespace DomainModeling.Core {
    /// <summary>
    /// A Domain Event is a special type of Value Object that represents an event that
    /// has occured with in the domain.
    /// </summary>
    public interface DomainEvent {
        DateTime OccurredAtUtc { get; }
        DateTime ProcessedAtUtc { get; }
    }
}
