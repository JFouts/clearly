namespace DomainModeling.Crud.WebUi.ViewModels;

public record InputFieldEditorViewModel {
    public string Id { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}