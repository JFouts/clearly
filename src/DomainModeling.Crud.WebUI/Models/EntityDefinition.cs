namespace DomainModeling.Crud.WebUi.Models;

public record EntityFormDefinition
{
    public Type EntityType { get; set; } = typeof(object);
    public string EntityName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DataSourceUrl { get; set; } = string.Empty;
    public IEnumerable<EditorFormFieldDefinition> Fields {get; set; } = new EditorFormFieldDefinition[0];
}
