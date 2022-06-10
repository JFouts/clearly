// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Concurrent;

namespace Clearly.Crud;

public class EntityDefinitionFactory : IEntityDefinitionFactory
{
    private readonly IEnumerable<IEntityModule> entityModules;
    private readonly IEnumerable<IEntityFieldModule> entityFieldModules;
    private readonly ConcurrentDictionary<Type, EntityDefinition> entityPool = new ConcurrentDictionary<Type, EntityDefinition>();

    public EntityDefinitionFactory(IEnumerable<IEntityModule> entityModules, IEnumerable<IEntityFieldModule> entityFieldModules)
    {
        this.entityModules = entityModules;
        this.entityFieldModules = entityFieldModules;
    }

    public EntityDefinition CreateForType(Type entity)
    {
        if (entityPool.TryGetValue(entity, out var definition))
        {
            return definition;
        }

        definition = new EntityDefinition
        {
            ObjectType = entity,
            NameKey = entity.Name,
            DisplayName = entity.Name.FormatForDisplay(),
            Fields = entity.GetProperties().Select(x => new FieldDefinition(x)).ToList(),
        };

        ApplyModules(definition);
        ApplyFallbackDefaults(definition);

        entityPool[entity] = definition;

        return definition;
    }

    private void ApplyModules(EntityDefinition definition)
    {
        foreach (var module in entityModules)
        {
            module.OnApplyingModule(definition);
        }

        foreach (var field in definition.Fields)
        {
            foreach (var module in entityFieldModules)
            {
                module.OnApplyingModule(definition, field);
            }
        }
    }

    private void ApplyFallbackDefaults(EntityDefinition definition)
    {
        foreach (var module in entityModules)
        {
            module.OnApplyingFallbackDefaults(definition);
        }

        foreach (var field in definition.Fields)
        {
            foreach (var module in entityFieldModules)
            {
                module.OnApplyingFallbackDefaults(definition, field);
            }
        }
    }
}
