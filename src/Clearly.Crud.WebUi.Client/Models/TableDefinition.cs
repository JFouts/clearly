namespace Clearly.Crud.WebUi.Client.Models
{
    public class TableDefinition
    {
        public string NameKey { get; set; } = string.Empty;
        public IEnumerable<ColumnDefinition> Columns { get; set; } = Array.Empty<ColumnDefinition>();
    }
}
