namespace DomainModeling.Crud;

public interface IEntityFieldModule : IModule
{
    void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field);
    void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field);
}
