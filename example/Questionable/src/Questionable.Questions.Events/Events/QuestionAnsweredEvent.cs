using System;
using DomainModeling.Core.Interfaces;

namespace Questionable.Questions.Events.Events
{
    public class QuestionAnsweredEvent : IDomainEvent
    {
        public Guid Id { get; }
        public Guid AnswerId { get; }
        public Guid UserId { get; }
        public string Description { get; }

        public DateTime OccurredAtUtc { get; }

        public DateTime ProcessedAtUtc { get; }

        public QuestionAnsweredEvent(Guid id, Guid answerId, Guid userId, string description, DateTime occurredAtUtc, DateTime processedAtUtc)
        {
            Id = id;
            AnswerId = answerId;
            UserId = userId;
            Description = description;
            OccurredAtUtc = occurredAtUtc;
            ProcessedAtUtc = processedAtUtc;
        }
    }
}