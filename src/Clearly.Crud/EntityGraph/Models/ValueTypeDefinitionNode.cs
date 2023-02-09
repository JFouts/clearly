// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a data type that is a value type.
/// </summary>
public class ValueTypeDefinitionNode : TypeDefinitionNode
{
    public ValueTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}
