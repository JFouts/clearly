using System;
using DomainModeling.Core;

namespace Questionable.Questions.Commands.Commands
{
    public class LikeQuestionCommand : Command
    {
        public Guid QuestionId { get; }

        public Guid UserId { get; }

        public DateTime OccurredAtUtc { get; }

        public LikeQuestionCommand(Guid questionId, Guid userId, DateTime occurredAtUtc)
        {
            QuestionId = questionId;
            UserId = userId;
            OccurredAtUtc = occurredAtUtc;
        }
    }
}
