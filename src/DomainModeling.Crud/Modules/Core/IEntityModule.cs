namespace DomainModeling.Crud;

public interface IEntityModule : IModule
{
    void OnApplyingModule(EntityDefinition entity);
    void OnApplyingFallbackDefaults(EntityDefinition entity);
}
