using Clearly.Core.Interfaces;
using Clearly.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Commands.CommandHandlers;

public class LikeQuestionCommandHandler : ICommandHandler<LikeQuestionCommand>
{
    private readonly IEventSourcedAggregateRepository<Question> _aggregateRepository;

    public LikeQuestionCommandHandler(IEventSourcedAggregateRepository<Question> aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task ExecuteAsync(LikeQuestionCommand command)
    {
        var question = await _aggregateRepository.RetrieveAsync(command.QuestionId);

        if (question.State.UserId == command.UserId)
            // TODO: Better Exception
            throw new Exception();

        if (question.State.LikedUsers.Any(x => x == command.UserId))
            // TODO: Better Exception
            throw new Exception();

        await question.FireEventAsync(new QuestionLikedEvent(command.QuestionId, command.UserId, command.OccurredAtUtc, DateTime.UtcNow));
        await question.SaveAsync();
    }
}
