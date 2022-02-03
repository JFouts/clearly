using DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

namespace DomainModeling.Crud.WebUi.Models;

public record EntityFormDefinition
{
    public string EntityName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string DataSourceUrl { get; set; } = string.Empty;
    public IEnumerable<EditorFormFieldDefinition> Fields {get; set; } = new EditorFormFieldDefinition[0];
}

public record EditorFormFieldDefinition
{
    public string FieldName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public EditorFormFieldType FieldType { get; set; } = new EditorFormFieldType { FieldEditorName = nameof(InputFieldEditor) };
}

public record EditorFormFieldType
{
    public string FieldEditorName { get; set; }  = string.Empty;
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}
