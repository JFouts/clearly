using DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

namespace DomainModeling.Crud.WebUi.Models;

public record EditorFormFieldDefinition
{
    public string FieldName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public EditorFormFieldType FieldType { get; set; } = new EditorFormFieldType { FieldEditorName = nameof(InputFieldEditor) };
}
