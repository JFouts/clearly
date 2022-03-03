using System;
using System.ComponentModel.DataAnnotations;
using Clearly.Core;
using Clearly.Core.ValidationAnnotations;

namespace Questionable.Questions.Commands.Commands
{
    public class AnswerQuestionCommand : Command
    {
        public Guid QuestionId { get; }

        public DateTime OccurredAtUtc { get; }

        public Guid AnswerId { get; }

        public Guid UserId { get; }

        [Required]
        [NonEmpty]
        public string Description { get; }

        public AnswerQuestionCommand(Guid questionId, DateTime occurredAtUtc, Guid answerId, Guid userId, string description)
        {
            QuestionId = questionId;
            OccurredAtUtc = occurredAtUtc;
            AnswerId = answerId;
            UserId = userId;
            Description = description;
        }
    }
}
