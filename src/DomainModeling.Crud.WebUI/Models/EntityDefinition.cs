using DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

namespace DomainModeling.Crud.WebUi.Models;

public interface IPagableViewModel
{
    int PageCount { get; }
    int CurrentPage { get; }
}

public record EntitySearchModel : IPagableViewModel
{
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
    public IEnumerable<Dictionary<string, object>> Results { get; set; } = new Dictionary<string, object>[0];
    public EntityFormDefinition FormDefinition { get; set; } = new EntityFormDefinition();
}

public record EntityFormDefinition
{
    public Type EntityType { get; set; } = typeof(object);
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
