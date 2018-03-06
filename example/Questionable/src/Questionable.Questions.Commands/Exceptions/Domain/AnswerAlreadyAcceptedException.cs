using System;

namespace Questionable.Questions.Commands.Exceptions.Domain
{
    public class AnswerAlreadyAcceptedException : InvalidStateException
    {
        public Guid QuestionId { get; }

        public AnswerAlreadyAcceptedException(Guid questionId)
        {
            QuestionId = questionId;
        }
    }
}