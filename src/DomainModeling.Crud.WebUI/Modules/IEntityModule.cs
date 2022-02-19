namespace DomainModeling.Crud.WebUi;

public interface IEntityModule : IModule
{
    void OnApplyingModule(EntityDefinition entity);
}
