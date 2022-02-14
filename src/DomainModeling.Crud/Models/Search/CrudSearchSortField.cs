namespace DomainModeling.Crud;

public class CrudSearchSortField
{
    public string Field { get; set; } = string.Empty;
    public CrudSearchSortDirection Direction { get; set; } = CrudSearchSortDirection.Ascending;
}
