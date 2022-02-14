namespace DomainModeling.Crud;

public class CrudSearchOptions
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public CrudSearchFilter Filters { get; set; } = new CrudSearchFilter();
    public IEnumerable<CrudSearchSortField> SortFields { get; set; } = new List<CrudSearchSortField>();
}
