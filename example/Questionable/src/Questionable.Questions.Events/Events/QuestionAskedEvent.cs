using System;
using DomainModeling.Core.Interfaces;

namespace Questionable.Questions.Events.Events
{
    public class QuestionAskedEvent : IDomainEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime OccurredAtUtc { get; }
        public DateTime ProcessedAtUtc { get; }

        public QuestionAskedEvent(Guid id, Guid userId, string title, string description, DateTime occurredAtUtc, DateTime processedAtUtc)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Description = description;
            OccurredAtUtc = occurredAtUtc;
            ProcessedAtUtc = processedAtUtc;
        }
    }
}
