namespace DomainModeling.Crud.WebUi;

public interface IEntityDefinitionFactory
{
    EntityDefinition CreateForType(Type entity);
}
