namespace DomainModeling.Crud.WebUi;

public class CrudAdminEntityModule : EntityModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
        var metadata = entity.UsingMetadata<CrudAdminEntityMetadata>();

        if (string.IsNullOrWhiteSpace(metadata.DataSourceUrl))
        {
            metadata.DataSourceUrl = $"/api/{entity.NameKey}";
        }
    }
}
