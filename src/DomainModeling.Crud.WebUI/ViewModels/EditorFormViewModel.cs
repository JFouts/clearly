namespace DomainModeling.Crud.WebUi.ViewModels;

public record EditorFormViewModel
{
    public string EntityName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public IEnumerable<EditorFormFieldViewModel> Fields {get; set; } = new EditorFormFieldViewModel[0];
}

public record EditorFormFieldViewModel
{
    public string FieldName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public EditorFormFieldTypeViewModel FieldType { get; set; } = EditorFormFieldTypeViewModel.Input;
}

public enum EditorFormFieldTypeViewModel
{
    Input,
    Checkbox,
    RadioButtons,

}