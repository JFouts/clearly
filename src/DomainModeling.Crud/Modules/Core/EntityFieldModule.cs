namespace DomainModeling.Crud;

public abstract class EntityFieldModule : IEntityFieldModule
{
    public virtual void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field) { }
    public virtual void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field) { }
}
