using System;
using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;
using DomainModeling.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Commands.Exceptions.Domain;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Commands.CommandHandlers
{
    public class AnswerQuestionCommandHandler : ICommandHandler<AnswerQuestionCommand>
    {
        private readonly IEventSourcedAggregateRepository<Question> _aggregateRepository;

        public AnswerQuestionCommandHandler(IEventSourcedAggregateRepository<Question> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task ExecuteAsync(AnswerQuestionCommand command)
        {
            var question = await _aggregateRepository.RetrieveAsync(command.QuestionId);

            if (question.State.HasAcceptedAnswer)
                throw new AnswerAlreadyAcceptedException(question.State.Id);

            await question.FireEventAsync(
                new QuestionAnsweredEvent(
                    command.QuestionId,
                    command.AnswerId,
                    command.UserId,
                    command.Description,
                    command.OccurredAtUtc,
                    DateTime.UtcNow));
            await question.SaveAsync();
        }
    }
}
