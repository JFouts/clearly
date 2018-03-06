using System.Threading.Tasks;

namespace Questionable.Queries.QuestionSearch
{
    public interface IQuestionSearcher
    {
        Task<IPagedResult<PopularQuestion>> GetMostPopularQuestions(int skip, int take);
    }
}