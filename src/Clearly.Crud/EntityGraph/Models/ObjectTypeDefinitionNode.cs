// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="object"/>.
/// </summary>
public class ObjectTypeDefinitionNode : TypeDefinitionNode
{
    public IEnumerable<PropertyDefinitionNode> Properties { get; set; } = new List<PropertyDefinitionNode>();

    public ObjectTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}
