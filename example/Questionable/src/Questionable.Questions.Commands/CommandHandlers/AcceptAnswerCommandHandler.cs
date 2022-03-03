using System.Linq;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;
using Clearly.Core.Utilities.Interfaces;
using Clearly.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Commands.Exceptions.Domain;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Commands.CommandHandlers
{
    public class AcceptAnswerCommandHandler : ICommandHandler<AcceptAnswerCommand>
    {
        private readonly IEventSourcedAggregateRepository<Question> _aggregateRepository;
        private readonly IDate _date;

        public AcceptAnswerCommandHandler(IEventSourcedAggregateRepository<Question> aggregateRepository, IDate date)
        {
            _aggregateRepository = aggregateRepository;
            _date = date;
        }

        public async Task ExecuteAsync(AcceptAnswerCommand command)
        {
            var processedAtUtc = _date.CurrentDateUtc();
            var question = await _aggregateRepository.RetrieveAsync(command.QuestionId);

            if(question.State.UserId != command.UserId)
                throw new UserPermissionException();

            if (question.State.HasAcceptedAnswer)
                throw new AnswerAlreadyAcceptedException(command.QuestionId);

            if(question.State.Answers.All(x => x.AnswerId != command.AnswerId))
                throw new AnswerNotFoundException();

            await question.FireEventAsync(new AnswerAcceptedEvent(command.QuestionId, command.AnswerId, command.OccurredAtUtc, processedAtUtc));
            await question.SaveAsync();
        }
    }
}