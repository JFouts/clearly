namespace DomainModeling.Crud.WebUi;

/// <summary>
/// Sets the <see cref="ViewComponent"> the should be used to render editor for this field.
/// </summary>
/// <remarks>
/// For the list of system view components see TODO: URL
/// </remarks>
public class FieldEditorAttribute : EntityFieldDefinitionAttribute
{
    public string ViewComponentName { get; set; }

    public FieldEditorAttribute(string viewComponentName)
    {
        ViewComponentName = viewComponentName;
    }

    protected internal override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        var metadata = field.UsingMetadata<CrudAdminEntityFieldMetadata>();


        metadata.EditorViewComponentName = ViewComponentName;
    }
}
