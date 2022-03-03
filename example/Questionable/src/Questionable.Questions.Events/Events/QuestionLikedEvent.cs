using System;
using Clearly.Core.Interfaces;

namespace Questionable.Questions.Events.Events
{
    public class QuestionLikedEvent : IDomainEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }

        public DateTime OccurredAtUtc { get; }

        public DateTime ProcessedAtUtc { get; }

        public QuestionLikedEvent(Guid id, Guid userId, DateTime occurredAtUtc, DateTime processedAtUtc)
        {
            Id = id;
            UserId = userId;
            OccurredAtUtc = occurredAtUtc;
            ProcessedAtUtc = processedAtUtc;
        }
    }
}
