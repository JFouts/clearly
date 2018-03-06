using System.Threading.Tasks;
using DomainModeling.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Events.EventHandlers
{
    public class QuestionLikedHandler : DomainEventHandler<Question, QuestionLikedEvent>
    {
        public override Task HandleAsync(Question question, QuestionLikedEvent @event)
        {
            question.Likes++;
            question.LikedUsers.Add(@event.UserId);
            return Task.CompletedTask;
        }
    }
}
