namespace DomainModeling.Crud;

public class CrudSearchResult<T>
{
    public int Count { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public IAsyncEnumerable<T> Results { get; set; } = AsyncEnumerable.Empty<T>();
}

public class CrudSearchOptions
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public CrudSearchFilter Filters { get; set; } = new CrudSearchFilter();
    public IEnumerable<CrudSearchSortField> SortFields { get; set; } = new List<CrudSearchSortField>();
}

public class CrudSearchFilter
{
    public string FilterCondition { get; set; } = string.Empty;
}

public class CrudSearchSortField
{
    public string Field { get; set; } = string.Empty;
    public CrudSearchSortDirection Direction { get; set; } = CrudSearchSortDirection.Ascending;
}

public enum CrudSearchSortDirection
{
    Ascending = 1,
    Descending = 2
}