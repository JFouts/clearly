// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a data type that is an <see cref="IEntity"/>.
/// </summary>
public class EntityTypeDefinitionNode : ObjectTypeDefinitionNode
{
    public EntityTypeDefinitionNode(Type type)
        : base(type)
    {
    }
}
