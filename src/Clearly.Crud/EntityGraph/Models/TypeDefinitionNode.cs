// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a type.
/// </summary>
public abstract class TypeDefinitionNode : DefinitionNode
{
    /// <summary>
    /// Gets the Type this node represents.
    /// </summary>
    public Type Type { get; }

    public TypeDefinitionNode(Type type)
    {
        Type = type;
    }
}
