namespace DomainModeling.Crud.WebUi;

public abstract class EntityModule : IEntityModule
{
    public abstract void OnApplyingModule(EntityDefinition entity);
}
