using System;

namespace DomainModeling.Core.DomainObjectTypes {
  /// <summary>
  /// A Domain Event is a special type of Value Object that represents
  /// an event that has occured with in the domain.
  /// </summary>
  public interface DomainEvent {
    /// <summary>
    /// Despite Domain Events being a Value Objects and immutable we
    /// use an identity to uniquely identify Domain Events so that
    /// they are not processed multiple times
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The date and time in which the event occured in real time
    /// </summary>
    DateTime OccurredAtUtc { get; }

    /// <summary>
    /// The date and time in which the event was recieved and processed
    /// by the application in system time.
    /// </summary>
    DateTime ProcessedAtUtc { get; }
  }
}
