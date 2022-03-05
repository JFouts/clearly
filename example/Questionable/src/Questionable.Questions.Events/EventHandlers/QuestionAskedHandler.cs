using Clearly.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Events.EventHandlers;

public class QuestionAskedHandler : DomainEventHandler<Question, QuestionAskedEvent>
{
    public override Task HandleAsync(Question question, QuestionAskedEvent @event)
    {
        question.Title = @event.Title;
        question.Description = @event.Description;
        question.UserId = @event.UserId;
        question.Likes = 0;
        question.HasAcceptedAnswer = false;

        return Task.CompletedTask;
    }
}
