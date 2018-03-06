using System.Linq;
using System.Threading.Tasks;
using Questionable.Queries.Models;
using Repositoy.Core;

namespace Questionable.Queries.QuestionSearch
{
    public class QuestionSearcher : IQuestionSearcher
    {
        private readonly IQuery<Question> _questions;

        public QuestionSearcher(IQuery<Question> questions)
        {
            _questions = questions;
        }

        public async Task<IPagedResult<PopularQuestion>> GetMostPopularQuestions(int skip, int take)
        {
            var query =
                from question in _questions.Query
                orderby question.Likes descending
                select new PopularQuestion(question);

            return await Paged(query, skip, take);
        }

        private static Task<IPagedResult<T>> Paged<T>(IQueryable<T> query, int skip, int take)
        {
            var count = query.Count();

            return Task.FromResult<IPagedResult<T>>(new PagedResult<T>
            {
                Count = count,
                Skip = skip,
                Take = take,
                Results = query
            });
        }
    }
}
