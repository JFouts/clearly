using System.Linq;
using System.Threading.Tasks;
using DomainModeling.EventSubscription.EventStore;
using Questionable.Queries.Models;
using Questionable.Questions.Events.Events;
using Repositoy.Core;

namespace Questionable.Queries.Denormalizers.QuestionDetails
{
    [Subscription("$ce-Question")]
    public class QuestionDetailDenormalizer
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionDetailDenormalizer(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [Handler]
        public async Task QuestionAsked(QuestionAskedEvent @event)
        {
            var question = new Question
            {
                Description = @event.Description,
                Id = @event.Id,
                Likes = 0,
                Title = @event.Title
            };

            await _questionRepository.CreateAsync(question.Id, question);
        }

        [Handler]
        public async Task QuestionAnswered(QuestionAnsweredEvent @event)
        {
            var question = await _questionRepository.GetByIdAsync(@event.Id);

            question.Data.Answers.Add(new Question.Answer
            {
                AnswerId = @event.AnswerId
            });

            await _questionRepository.UpdateAsync(question.Data.Id, question);
        }

        [Handler]
        public async Task QuestionLiked(QuestionLikedEvent @event)
        {
            var question = await _questionRepository.GetByIdAsync(@event.Id);

            question.Data.Likes++;

            await _questionRepository.UpdateAsync(question.Data.Id, question);
        }

        [Handler]
        public async Task AnswerAccepted(AnswerAcceptedEvent @event)
        {
            var question = await _questionRepository.GetByIdAsync(@event.Id);

            question.Data.Answers.First(x => x.AnswerId == @event.AnswerId).Accepted = true;

            await _questionRepository.UpdateAsync(question.Data.Id, question);
        }
    }
}
