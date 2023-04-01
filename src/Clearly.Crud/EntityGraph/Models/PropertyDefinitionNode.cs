// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in a definition graph for a property of an object.
/// </summary>
public class PropertyDefinitionNode : DefinitionNode
{
    /// <summary>
    /// Gets the <see cref="PropertyInfo" /> this node represents.
    /// </summary>
    public PropertyInfo Property { get; }

    /// <summary>
    /// Gets the <see cref="ObjectTypeDefinitionNode"/> representing the type of the property this node represents.
    /// </summary>
    public ObjectTypeDefinitionNode Type { get; }

    public PropertyDefinitionNode(PropertyInfo property, ObjectTypeDefinitionNode type)
    {
        Property = property;
        Type = type;
    }
}
