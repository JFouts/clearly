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

public class AskQuestionTests : CommandTestBase<AskQuestionCommandHandler, AskQuestionCommand>
{
    private readonly List<AskQuestionCommand.QuestionSubjectTag> tags = new List<AskQuestionCommand.QuestionSubjectTag>();
    private readonly Guid questionId = Guid.Parse("136a81cd-f244-457f-bacc-72519bc6589c");
    private readonly Guid userId = Guid.Parse("336a81cd-f244-457f-bacc-72519bc6589c");
    private readonly MockAggregate<Question> question = new MockAggregate<Question>();
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
        await WhenAUserAttemptsToAskTheQuestion();
        ThenItProducesAn<InvalidCommandException>();
    }

    [Fact]
    public async Task CreatesNewStreamWhenQuestionAsked()
    {
        GivenANewQuestion();
        await WhenAUserAttemptsToAskTheQuestion();
        ThenTheNewQuestionIsAsked();
    }

    private void GivenANewQuestion()
    {
        question.State = new Question();
    }

    private void GivenANewQuestionWithRepeatedTags()
    {
        question.State = new Question();

        const string tag = "example";
        tags.Add(new AskQuestionCommand.QuestionSubjectTag(tag));
        tags.Add(new AskQuestionCommand.QuestionSubjectTag(tag));
    }

    private async Task WhenAUserAttemptsToAskTheQuestion()
    {
        await ExceptionWrappedAsync(async () =>
        {
            await Handler!.ExecuteAsync(new AskQuestionCommand(questionId, EventTime, userId, Title, Description, tags));
        });
    }

    private void ThenTheNewQuestionIsAsked()
    {
        AssertContains<IDomainEvent, QuestionAskedEvent>(question.SavedEvents, x =>
            x.Id == questionId &&
            x.UserId == userId &&
            x.Title == Title &&
            x.Description == Description &&
            x.OccurredAtUtc == EventTime &&
            x.ProcessedAtUtc == CurrentTime);
    }

    private IEventSourcedAggregateRepository<Question> CreateMockRepository()
    {
        var r = new Mock<IEventSourcedAggregateRepository<Question>>(MockBehavior.Strict);
        r.Setup(x => x.Instantiate(questionId)).Returns(question);
        
        return r.Object;
    }
}
