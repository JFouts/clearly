namespace DomainModeling.Crud;

public abstract class EntityModule : IEntityModule
{
    public virtual void OnApplyingModule(EntityDefinition entity) { }
    public virtual void OnApplyingFallbackDefaults(EntityDefinition entity) { }
}
