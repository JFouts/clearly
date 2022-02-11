namespace DomainModeling.Crud.WebUi.Models;

public record EditorFormFieldType
{
    public string FieldEditorName { get; set; }  = string.Empty;
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}
