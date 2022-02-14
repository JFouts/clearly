namespace DomainModeling.Crud;

public class CrudSearchResult<T>
{
    public int Count { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public IAsyncEnumerable<T> Results { get; set; } = AsyncEnumerable.Empty<T>();
}
