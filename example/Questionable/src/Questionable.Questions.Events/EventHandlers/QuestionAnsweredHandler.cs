using System.Threading.Tasks;
using DomainModeling.EventSourcing;
using Questionable.Questions.Aggregates;
using Questionable.Questions.Events.Events;

namespace Questionable.Questions.Events.EventHandlers
{
    public class QuestionAnsweredHandler : DomainEventHandler<Question, QuestionAnsweredEvent>
    {
        public override Task HandleAsync(Question question, QuestionAnsweredEvent @event)
        {
            question.Answers.Add(new Question.Answer
            {
                AnswerId = @event.AnswerId,
                Description = @event.Description,
                UserId = @event.UserId
            });

            return Task.CompletedTask;
        }
    }
}
