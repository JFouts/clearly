using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;
using DomainModeling.Core.Utilities.Interfaces;
using DomainModeling.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Commands.Exceptions.Domain;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Commands.CommandHandlers
{
    public class AskQuestionCommandHandler : ICommandHandler<AskQuestionCommand>
    {
        private readonly IEventSourcedAggregateRepository<Question> _aggregateRepository;
        private readonly IDate _date;

        public AskQuestionCommandHandler(IEventSourcedAggregateRepository<Question> aggregateRepository, IDate date)
        {
            _aggregateRepository = aggregateRepository;
            _date = date;
        }

        public async Task ExecuteAsync(AskQuestionCommand command)
        {
            var processTimeUtc = _date.CurrentDateUtc();

            AssertSubjectTagsAreUnique(command.SubjectsTags);

            var question = _aggregateRepository.Instantiate(command.QuestionId);
            await question.FireEventAsync(new QuestionAskedEvent(
                command.QuestionId,
                command.UserId,
                command.Title,
                command.Description,
                command.OccurredAtUtc,
                processTimeUtc));

            foreach(var subjectTag in command.SubjectsTags)
            {
                await question.FireEventAsync(new QuestionTaggedEvent(
                    Guid.NewGuid(),
                    subjectTag.SubjectTag,
                    command.OccurredAtUtc,
                    processTimeUtc));
            }

            await question.SaveAsync();
        }

        private static void AssertSubjectTagsAreUnique(IEnumerable<AskQuestionCommand.QuestionSubjectTag> tags)
        {
            if (!IsUnique(tags.Select(x => x.SubjectTag)))
                throw new RepeatedSubjectTagsException();
        }

        private static bool IsUnique<T>(IEnumerable<T> collection)
        {
            var list = collection.ToList();
            return list.Distinct().Count() == list.Count;
        }
    }
}