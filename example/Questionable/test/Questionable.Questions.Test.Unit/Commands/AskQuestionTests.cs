using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;
using Clearly.EventSourcing;
using Moq;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.CommandHandlers;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Commands.Exceptions;
using Questionable.Questions.Events.Events;
using Xunit;

namespace Questionable.Questions.Test.Unit.Commands
{
    public class AskQuestionTests : CommandTestBase<AskQuestionCommandHandler, AskQuestionCommand>
    {
        private readonly List<AskQuestionCommand.QuestionSubjectTag> _tags = new List<AskQuestionCommand.QuestionSubjectTag>();
        private readonly Guid _questionId = Guid.Parse("136a81cd-f244-457f-bacc-72519bc6589c");
        private readonly Guid _userId = Guid.Parse("336a81cd-f244-457f-bacc-72519bc6589c");
        private readonly MockAggregate<Question> _question = new MockAggregate<Question>();
        private const string Title = "Example Title";
        private const string Description = "Example Description";

        public AskQuestionTests()
        {
            var repo = CreateMockRepository();
            var date = CreateMockDate();

            Handler = new AskQuestionCommandHandler(repo, date);
        }

        [Fact]
        public async Task FailsWhenQuestionHasRepeatedTags()
        {
            GivenANewQuestionWithRepeatedTags();
            await WhenAUserAttepmtsToAskTheQuestion();
            ThenItProducesAn<InvalidCommandException>();
        }

        [Fact]
        public async Task CreatesNewStreamWhenQuestionAsked()
        {
            GivenANewQuestion();
            await WhenAUserAttepmtsToAskTheQuestion();
            ThenTheNewQuestionIsAsked();
        }

        private void GivenANewQuestion()
        {
            _question.State = new Question();
        }

        private void GivenANewQuestionWithRepeatedTags()
        {
            _question.State = new Question();

            const string tag = "example";
            _tags.Add(new AskQuestionCommand.QuestionSubjectTag(tag));
            _tags.Add(new AskQuestionCommand.QuestionSubjectTag(tag));
        }

        private async Task WhenAUserAttepmtsToAskTheQuestion()
        {
            await ExceptionWrappedAsync(async () =>
            {
                await Handler.ExecuteAsync(new AskQuestionCommand(_questionId, EventTime, _userId, Title, Description, _tags));
            });
        }

        private void ThenTheNewQuestionIsAsked()
        {
            AssertContains<IDomainEvent, QuestionAskedEvent>(_question.SavedEvents, x =>
                x.Id == _questionId &&
                x.UserId == _userId &&
                x.Title == Title &&
                x.Description == Description &&
                x.OccurredAtUtc == EventTime &&
                x.ProcessedAtUtc == CurrentTime);
        }

        private IEventSourcedAggregateRepository<Question> CreateMockRepository()
        {
            var r = new Mock<IEventSourcedAggregateRepository<Question>>(MockBehavior.Strict);
            r.Setup(x => x.Instantiate(_questionId)).Returns(_question);
            return r.Object;
        }
    }
}
