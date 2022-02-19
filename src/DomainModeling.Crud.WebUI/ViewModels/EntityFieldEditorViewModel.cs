namespace DomainModeling.Crud.WebUi.ViewModels;

public record EntityFieldEditorViewModel
{
    public EntityFieldDefinition Definition { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string FieldEditorName { get; set; } = string.Empty;
    public object? Value { get; set; } // TODO: Options for boxing?

    public EntityFieldEditorViewModel(EntityFieldDefinition definition)
    {
        Definition = definition;
    }
}