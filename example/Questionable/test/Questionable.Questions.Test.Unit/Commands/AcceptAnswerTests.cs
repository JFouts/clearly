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
    public class AcceptAnswerTests : CommandTestBase<AcceptAnswerCommandHandler, AcceptAnswerCommand>
    {
        private readonly Guid _questionId = Guid.Parse("136a81cd-f244-457f-bacc-72519bc6589c");
        private Guid _answerId = Guid.Parse("236a81cd-f244-457f-bacc-72519bc6589c");
        private readonly Guid _userId = Guid.Parse("336a81cd-f244-457f-bacc-72519bc6589c");
        private readonly MockAggregate<Question> _question = new MockAggregate<Question>();

        public AcceptAnswerTests()
        {
            var repository = CreateMockRepository();
            var date = CreateMockDate();

            Handler = new AcceptAnswerCommandHandler(repository, date);
        }

        [Fact]
        public async Task FailsWhenQuestionHasAnAcceptedAnswer()
        {
            GivenAQuestionWithAnAcceptedAnswer();
            await WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion();
            ThenItProducesAn<InvalidStateException>();
        }

        [Fact]
        public async Task FailsWhenUserDidntAskTheQuestion()
        {
            GivenAQuestionAndAUserThatDidntAskTheQuestion();
            await WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion();
            ThenItProducesAn<PermissionException>();
        }

        [Fact]
        public async Task FailsWhenAnswerDoesNotExist()
        {
            GivenAQuestion();
            await WhenAUserAttemptsToAcceptAnAnswerThatIsNotOnThatQuestion();
            ThenItProducesAn<NotFoundException>();
        }

        [Fact]
        public async Task CreatesEventWhenAnswerAccepted()
        {
            GivenAQuestion();
            await WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion();
            ThenTheAnswerHasBeenAccepted();
        }

        private void GivenAQuestion()
        {
            _question.State = new Question
            {
                UserId = _userId,
                Answers = new List<Question.Answer>
                {
                    new Question.Answer
                    {
                        AnswerId = _answerId
                    }
                }
            };
        }

        private void GivenAQuestionWithAnAcceptedAnswer()
        {
            _question.State = new Question
            {
                HasAcceptedAnswer = true,
                UserId = _userId
            };
        }

        private void GivenAQuestionAndAUserThatDidntAskTheQuestion()
        {
            _question.State = new Question
            {
                UserId = Guid.Parse("996a81fd-f244-457f-bacc-72519bc6589c")
            };
        }

        private async Task WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion()
        {
            await AcceptAnswerOnQuestion();
        }

        private async Task WhenAUserAttemptsToAcceptAnAnswerThatIsNotOnThatQuestion()
        {
            _answerId = Guid.Parse("996a81fd-f244-457f-bacc-72519bc6589c");
            await AcceptAnswerOnQuestion();
        }


        private void ThenTheAnswerHasBeenAccepted()
        {
            AssertContains<IDomainEvent, AnswerAcceptedEvent>(_question.SavedEvents, x =>
                x.Id == _questionId &&
                x.AnswerId == _answerId &&
                x.OccurredAtUtc == EventTime &&
                x.ProcessedAtUtc == CurrentTime);
        }

        private async Task AcceptAnswerOnQuestion()
        {
            await ExceptionWrappedAsync(async () =>
            {
                await Handler.ExecuteAsync(new AcceptAnswerCommand(_questionId, EventTime, _answerId, _userId));
            });
        }

        private IEventSourcedAggregateRepository<Question> CreateMockRepository()
        {
            var r = new Mock<IEventSourcedAggregateRepository<Question>>(MockBehavior.Strict);
            r.Setup(x => x.RetrieveAsync(_questionId)).ReturnsAsync(_question);
            return r.Object;
        }
    }
}
