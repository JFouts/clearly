using Clearly.Core.Interfaces;

namespace Questionable.Questions.Events.Events;

public class QuestionTaggedEvent : IDomainEvent
{
    public Guid Id { get; }
    public string SubjectTag { get; }
    public DateTime OccurredAtUtc { get; }
    public DateTime ProcessedAtUtc { get; }

    public QuestionTaggedEvent(Guid id, string subjectTag, DateTime occurredAtUtc, DateTime processedAtUtc)
    {
        Id = id;
        SubjectTag = subjectTag;
        OccurredAtUtc = occurredAtUtc;
        ProcessedAtUtc = processedAtUtc;
    }
}
