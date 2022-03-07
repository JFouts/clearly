
namespace Questionable.Queries.QuestionSearch;

public interface IPagedResult<out T>
{
    int Count { get; }
    int Skip { get; }
    int Take { get; }
    IEnumerable<T> Results { get; }
}
