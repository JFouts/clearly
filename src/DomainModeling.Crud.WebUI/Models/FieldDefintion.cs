namespace DomainModeling.Crud.WebUi.Models;

public record FieldDefintion
{
    public string FieldName { get; set; } = string.Empty;
    public string FieldDisplayType { get; set; }  = string.Empty;
    public string FieldEditorType { get; set; }  = string.Empty;
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
}
