using Clearly.Core.Interfaces;
using Clearly.EventSourcing;
using Moq;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.CommandHandlers;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Commands.Exceptions;
using Questionable.Questions.Events.Events;
using Xunit;

namespace Questionable.Questions.Test.Unit.Commands;

public class AcceptAnswerTests : CommandTestBase<AcceptAnswerCommandHandler, AcceptAnswerCommand>
{
    private readonly Guid questionId = Guid.Parse("136a81cd-f244-457f-bacc-72519bc6589c");
    private Guid answerId = Guid.Parse("236a81cd-f244-457f-bacc-72519bc6589c");
    private readonly Guid userId = Guid.Parse("336a81cd-f244-457f-bacc-72519bc6589c");
    private readonly MockAggregate<Question> question = new MockAggregate<Question>();

    public AcceptAnswerTests()
    {
        var repo = CreateMockRepository();
        var date = CreateMockDate();

        Handler = new AcceptAnswerCommandHandler(repo, date);
    }

    [Fact]
    public async Task FailsWhenQuestionHasAnAcceptedAnswer()
    {
        GivenAQuestionWithAnAcceptedAnswer();
        await WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion();
        ThenItProducesAn<InvalidStateException>();
    }

    [Fact]
    public async Task FailsWhenUserDidNotAskTheQuestion()
    {
        GivenAQuestionAndAUserThatDidNotAskTheQuestion();
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
        question.State = new Question
        {
            UserId = userId,
            Answers = new List<Question.Answer>
                {
                    new Question.Answer
                    {
                        AnswerId = answerId
                    }
                }
        };
    }

    private void GivenAQuestionWithAnAcceptedAnswer()
    {
        question.State = new Question
        {
            HasAcceptedAnswer = true,
            UserId = userId,
        };
    }

    private void GivenAQuestionAndAUserThatDidNotAskTheQuestion()
    {
        question.State = new Question
        {
            UserId = Guid.Parse("996a81fd-f244-457f-bacc-72519bc6589c"),
        };
    }

    private async Task WhenAUserAttemptsToAcceptAnAnswerOnThatQuestion()
    {
        await AcceptAnswerOnQuestion();
    }

    private async Task WhenAUserAttemptsToAcceptAnAnswerThatIsNotOnThatQuestion()
    {
        answerId = Guid.Parse("996a81fd-f244-457f-bacc-72519bc6589c");
        await AcceptAnswerOnQuestion();
    }

    private void ThenTheAnswerHasBeenAccepted()
    {
        AssertContains<IDomainEvent, AnswerAcceptedEvent>(question.SavedEvents, x =>
            x.Id == questionId &&
            x.AnswerId == answerId &&
            x.OccurredAtUtc == EventTime &&
            x.ProcessedAtUtc == CurrentTime);
    }

    private async Task AcceptAnswerOnQuestion()
    {
        await ExceptionWrappedAsync(async () =>
        {
            await Handler!.ExecuteAsync(new AcceptAnswerCommand(questionId, EventTime, answerId, userId));
        });
    }

    private IEventSourcedAggregateRepository<Question> CreateMockRepository()
    {
        var r = new Mock<IEventSourcedAggregateRepository<Question>>(MockBehavior.Strict);
        r.Setup(x => x.RetrieveAsync(questionId)).ReturnsAsync(question);

        return r.Object;
    }
}
