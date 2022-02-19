namespace DomainModeling.Crud.WebUi;

public class EntityDefinitionFactory : IEntityDefinitionFactory
{
    private readonly IEnumerable<IEntityModule> _entityModules;
    private readonly IEnumerable<IEntityFieldModule> _entityFieldModules;

    public EntityDefinitionFactory(IEnumerable<IEntityModule> entityModules, IEnumerable<IEntityFieldModule> entityFieldModules)
    {
        _entityModules = entityModules;
        _entityFieldModules = entityFieldModules;
    }

    public EntityDefinition CreateForType(Type entity)
    {
        // TODO: Flyweight cache these?
        var definition = new EntityDefinition(entity);

        foreach (var module in _entityModules)
        {
            module.OnApplyingModule(definition);
        }

        foreach (var field in definition.Fields)
        {
            foreach (var module in _entityFieldModules)
            {
                module.OnApplyingModule(definition, field);
            }
        }

        return definition;
    }
}
