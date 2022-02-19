namespace DomainModeling.Crud.WebUi;

public interface IEntityFieldModule : IModule
{
    void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field);
}
