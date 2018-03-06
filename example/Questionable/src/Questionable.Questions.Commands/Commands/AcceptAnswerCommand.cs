using System;
using DomainModeling.Core;

namespace Questionable.Questions.Commands.Commands
{
    public class AcceptAnswerCommand : Command
    {
        public Guid QuestionId { get; }

        public DateTime OccurredAtUtc { get; }

        public Guid AnswerId { get; }

        public Guid UserId { get; }

        public AcceptAnswerCommand(Guid questionId, DateTime occurredAtUtc, Guid answerId, Guid userId)
        {
            QuestionId = questionId;
            OccurredAtUtc = occurredAtUtc;
            AnswerId = answerId;
            UserId = userId;
        }
    }
}
