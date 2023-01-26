// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Concurrent;
using System.Reflection;
using Clearly.Core;

namespace Clearly.Crud.Models.EntityGraph;

public class EntityDefinitionGraphFactory : IEntityDefinitionGraphFactory
{
    private readonly IEnumerable<IDefinitionNodeModule> _modules;
    private readonly ConcurrentDictionary<Type, ObjectTypeDefinitionNode> typeDefinitionPool = new ();

    public EntityDefinitionGraphFactory(IEnumerable<IDefinitionNodeModule> modules)
    {
        _modules = modules;
    }

    public ObjectTypeDefinitionNode CreateForType(Type type)
    {
        if (typeDefinitionPool.TryGetValue(type, out var poolNode))
        {
            return poolNode;
        }

        var rootNode = CreateForType(type, new Dictionary<Type, ObjectTypeDefinitionNode>());

        typeDefinitionPool.AddOrUpdate(type, rootNode, (_, existingNode) => rootNode);

        return rootNode;
    }

    public Dictionary<string, TypeDefinitionNodeFlattened> CreateFlattenedForType(Type type)
    {
        var root = CreateForType(type);
        return Flatten(root);
    }
    
    public Dictionary<string, TypeDefinitionNodeFlattened> Flatten(TypeDefinitionNode rootNode)
    {

        var flattenedNodes = new Dictionary<string, TypeDefinitionNodeFlattened>();

        Flatten(rootNode, flattenedNodes);

        return flattenedNodes;
    }

    private void Flatten(TypeDefinitionNode node, Dictionary<string, TypeDefinitionNodeFlattened> flattenedNodes)
    {
        if (flattenedNodes.ContainsKey(node.NodeKey))
        {
            return;
        }

        switch (node)
        {
            case EntityTypeDefinitionNode entityNode:
                FlattenObjectTypeNode(entityNode, new EntityTypeDefinitionNodeFlattened(), flattenedNodes);
                break;
            case ObjectTypeDefinitionNode objectNode:
                FlattenObjectTypeNode(objectNode, new ObjectTypeDefinitionNodeFlattened(), flattenedNodes);
                break;
            case ValueTypeDefinitionNode:
                FlattenTypeNode(node, new ValueTypeDefinitionNodeFlattened(), flattenedNodes);
                break;
            default:
                // TODO: Better errors
                throw new Exception();
        }
    }

    private void FlattenTypeNode(TypeDefinitionNode node, TypeDefinitionNodeFlattened flatNode, Dictionary<string, TypeDefinitionNodeFlattened> flattenedNodes)
    {
        flatNode.DisplayName = node.DisplayName;
        flatNode.NodeKey = node.NodeKey;
        flatNode.Features = node.RegisteredFeatures;

        flattenedNodes[node.NodeKey] = flatNode;
    }

    private void FlattenObjectTypeNode(ObjectTypeDefinitionNode node, ObjectTypeDefinitionNodeFlattened flatNode, Dictionary<string, TypeDefinitionNodeFlattened> flattenedNodes)
    {
        FlattenTypeNode(node, flatNode, flattenedNodes);

        flatNode.Properties = node.Properties.Select(x => new PropertyDefinitionNodeFlattened
        {
            DisplayName = x.DisplayName,
            NodeKey = x.NodeKey,
            TypeNodeKey = x.Type.NodeKey,
            Features = x.RegisteredFeatures,
        });

        foreach (var property in node.Properties)
        {
            Flatten(property.Type, flattenedNodes);
        }
    }

    protected ObjectTypeDefinitionNode CreateForType(Type type, Dictionary<Type, ObjectTypeDefinitionNode> visited)
    {
        if (visited.TryGetValue(type, out var visitedNode))
        {
            return visitedNode;
        }

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

        visited[type] = node;

        ApplyChildrenToType(node);

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
        foreach (var module in _modules)
        {
            module.OnApplyingModule(node);
        }
        
        foreach (var module in _modules)
        {
            module.OnApplyingFallbackDefaults(node);
        }
    }
}
