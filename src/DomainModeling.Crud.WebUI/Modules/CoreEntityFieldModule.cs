namespace DomainModeling.Crud.WebUi;

public class CoreEntityFieldModule : EntityFieldModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field)
    {
        if (string.IsNullOrWhiteSpace(field.DisplayName))
        {
            field.DisplayName = field.Property.Name.FormatForDisplay();
        }
    }
}
