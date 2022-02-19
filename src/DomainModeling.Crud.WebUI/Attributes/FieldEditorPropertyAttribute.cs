namespace DomainModeling.Crud.WebUi;

public class FieldEditorPropertyAttribute : EntityFieldDefinitionAttribute
{
    public string Name { get; set; } = string.Empty;
    public object Value { get; set; } = string.Empty;
    
    public FieldEditorPropertyAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }

    protected internal override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        var metadata = field.UsingMetadata<CrudAdminEntityFieldMetadata>();

        metadata.EditorProperties[Name] = Value;
    }
}