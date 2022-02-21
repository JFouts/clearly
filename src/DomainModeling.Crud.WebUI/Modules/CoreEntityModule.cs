namespace DomainModeling.Crud.WebUi;

public class CoreEntityModule : EntityModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
        if (string.IsNullOrWhiteSpace(entity.DisplayName))
        {
            entity.DisplayName = entity.Entity.Name.FormatForDisplay();
        }

        if (string.IsNullOrWhiteSpace(entity.NameKey))
        {
            entity.NameKey = entity.Entity.Name;
        }
    }
}
