using System.Linq;
using System.Threading.Tasks;
using Clearly.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Events.EventHandlers
{
    public class AnswerAcceptedHandler : DomainEventHandler<Question, AnswerAcceptedEvent>
    {
        public override Task HandleAsync(Question question, AnswerAcceptedEvent @event)
        {
            var acceptedAnswer = question.Answers.Single(x => x.AnswerId == @event.AnswerId);
            question.AcceptedAnswer = acceptedAnswer;
            question.HasAcceptedAnswer = true;

            return Task.CompletedTask;
        }
    }
}
