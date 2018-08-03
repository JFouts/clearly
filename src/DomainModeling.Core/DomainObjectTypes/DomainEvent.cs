using System;

namespace DomainModeling.Core.DomainObjectTypes {
    /// <summary>
    /// A Domain Event is a special type of Value Object that represents an event that
    /// has occured with in the domain.
    /// </summary>
    public interface DomainEvent {
        /// <summary>
        /// Dispate a Domain Events being a Value Object and immutable we use an identity
        /// to uniquely identify Domain Events so that they are not processed multiple times
        /// </summary>
        Guid Id { get; }
        DateTime OccurredAtUtc { get; }
        DateTime ProcessedAtUtc { get; }
    }
}
