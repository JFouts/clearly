// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;

namespace Clearly.Crud.EntityGraph;

public class EntityDefinitionGraphMapper : IEntityDefinitionGraphMapper
{
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
                FlattenObjectTypeNode(entityNode, new TypeDefinitionNodeFlattened { NodeType = TypeDefinitionNodeType.Entity }, flattenedNodes);
                break;
            case ObjectTypeDefinitionNode objectNode:
                FlattenObjectTypeNode(objectNode, new TypeDefinitionNodeFlattened { NodeType = TypeDefinitionNodeType.Object }, flattenedNodes);
                break;
            case ValueTypeDefinitionNode:
                FlattenTypeNode(node, new TypeDefinitionNodeFlattened { NodeType = TypeDefinitionNodeType.ValueType }, flattenedNodes);
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
        flatNode.Features = FlattenFeatures(node.RegisteredFeatures);

        flattenedNodes[node.NodeKey] = flatNode;
    }

    private void FlattenObjectTypeNode(ObjectTypeDefinitionNode node, TypeDefinitionNodeFlattened flatNode, Dictionary<string, TypeDefinitionNodeFlattened> flattenedNodes)
    {
        FlattenTypeNode(node, flatNode, flattenedNodes);

        flatNode.Properties = node.Properties.Select(x => new PropertyDefinitionNodeFlattened
        {
            DisplayName = x.DisplayName,
            NodeKey = x.NodeKey,
            TypeNodeKey = x.Type.NodeKey,
            Features = FlattenFeatures(x.RegisteredFeatures),
        }).ToList();

        foreach (var property in node.Properties)
        {
            Flatten(property.Type, flattenedNodes);
        }
    }

    private Dictionary<string, JToken> FlattenFeatures(IEnumerable<IDefinitionFeature> features)
    {
        return features.ToDictionary(x => x.GetType().Name.ToLowerInvariant(), x => JToken.FromObject(x));
    }
}
