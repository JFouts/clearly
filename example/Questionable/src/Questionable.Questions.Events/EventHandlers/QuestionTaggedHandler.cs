using System.Threading.Tasks;
using Clearly.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Events.EventHandlers
{
    public class QuestionTaggedHandler : DomainEventHandler<Question, QuestionTaggedEvent>
    {
        public override Task HandleAsync(Question question, QuestionTaggedEvent @event)
        {
            question.Tags.Add(@event.SubjectTag);
            return Task.CompletedTask;
        }
    }
}
