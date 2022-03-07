using Clearly.Core.Interfaces;

namespace Questionable.Questions.Events.Events;

public class AnswerAcceptedEvent : IDomainEvent
{
    public Guid Id { get; }
    public Guid AnswerId { get; }
    public DateTime OccurredAtUtc { get; }
    public DateTime ProcessedAtUtc { get; }

    public AnswerAcceptedEvent(Guid id, Guid answerId, DateTime occurredAtUtc, DateTime processedAtUtc)
    {
        Id = id;
        AnswerId = answerId;
        OccurredAtUtc = occurredAtUtc;
        ProcessedAtUtc = processedAtUtc;
    }
}
