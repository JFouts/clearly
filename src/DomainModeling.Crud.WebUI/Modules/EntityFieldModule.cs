namespace DomainModeling.Crud.WebUi;

public abstract class EntityFieldModule : IEntityFieldModule
{
    public abstract void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field);
}
