using System.Collections.Concurrent;

namespace DomainModeling.Crud.WebUi;

public class EntityDefinitionFactory : IEntityDefinitionFactory
{
    private readonly IEnumerable<IEntityModule> _entityModules;
    private readonly IEnumerable<IEntityFieldModule> _entityFieldModules;
    private readonly ConcurrentDictionary<Type, EntityDefinition> _entityPool = new ConcurrentDictionary<Type, EntityDefinition>();

    public EntityDefinitionFactory(IEnumerable<IEntityModule> entityModules, IEnumerable<IEntityFieldModule> entityFieldModules)
    {
        _entityModules = entityModules;
        _entityFieldModules = entityFieldModules;
    }

    public EntityDefinition CreateForType(Type entity)
    {
        if (_entityPool.TryGetValue(entity, out var definition))
        {
            return definition;
        }

        definition = new EntityDefinition(entity);

        ApplyModules(definition);
        ApplyFallbackDefaults(definition);

        _entityPool[entity] = definition;

        return definition;
    }

    private void ApplyModules(EntityDefinition definition)
    {
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
    }

    private void ApplyFallbackDefaults(EntityDefinition definition)
    {
        foreach (var module in _entityModules)
        {
            module.OnApplyingFallbackDefaults(definition);
        }

        foreach (var field in definition.Fields)
        {
            foreach (var module in _entityFieldModules)
            {
                module.OnApplyingFallbackDefaults(definition, field);
            }
        }
    }
}
