using System;

namespace DomainModeling.Core.Interfaces
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTime OccurredAtUtc { get; }
        DateTime ProcessedAtUtc { get; }
    }
}
