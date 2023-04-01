// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Concurrent;
using System.Reflection;
using Clearly.Core;

namespace Clearly.Crud.EntityGraph;

public class EntityDefinitionGraphFactory : IEntityDefinitionGraphFactory
{
    private readonly IEnumerable<IDefinitionNodeModule> modules;
    private readonly ConcurrentDictionary<Type, ObjectTypeDefinitionNode> typeDefinitionPool = new ();

    public EntityDefinitionGraphFactory(IEnumerable<IDefinitionNodeModule> modules)
    {
        this.modules = modules;
    }

    public ObjectTypeDefinitionNode CreateForType(Type type)
    {
        if (typeDefinitionPool.TryGetValue(type, out var poolNode))
        {
            return poolNode;
        }

        Console.WriteLine($"Creating for type {type.Name}");
        var rootNode = CreateForType(type, new Dictionary<Type, ObjectTypeDefinitionNode>());

        typeDefinitionPool.AddOrUpdate(type, rootNode, (_, existingNode) => rootNode);

        return rootNode;
    }

    protected ObjectTypeDefinitionNode CreateForType(Type type, Dictionary<Type, ObjectTypeDefinitionNode> visited)
    {
        if (visited.TryGetValue(type, out var visitedNode))
        {
            return visitedNode;
        }

        Console.WriteLine($"Visiting {type.Name}");

        ObjectTypeDefinitionNode node;

        if (type.IsAssignableTo(typeof(IEntity)))
        {
            node = new EntityTypeDefinitionNode(type);
        }
        else
        {
            node = new ObjectTypeDefinitionNode(type);
        }

        node.NodeKey = type.Name.ToCamelCase();
        node.DisplayName = type.Name.FormatForDisplay();


        Console.WriteLine($"Marking Visited {type.Name}");

        visited[type] = node;

        ApplyChildrenToType(node);

        Console.WriteLine($"Apply Modules for {type.Name}");

        ApplyModules(node);

        return node;
    }

    protected void ApplyChildrenToType(ObjectTypeDefinitionNode node)
    {
        var children = node.Type
            .GetProperties()
            .Select(CreatePropertyNode)
            .ToList();

        node.Properties = children;
    }

    protected PropertyDefinitionNode CreatePropertyNode(PropertyInfo property)
    {
        var node = new PropertyDefinitionNode(property, CreateForType(property.PropertyType))
        {
            DisplayName = property.Name.FormatForDisplay(),
            NodeKey = property.Name.ToCamelCase(),
        };

        ApplyModules(node);

        return node;
    }

    private void ApplyModules(DefinitionNode node)
    {
        foreach (var module in modules)
        {
            Console.WriteLine($"Applying Module {module.GetType().Name} to {node.DisplayName}:{node.GetType().Name}");
            module.OnApplyingModule(node);
        }
        
        foreach (var module in modules)
        {
            module.OnApplyingFallbackDefaults(node);
        }
    }
}
