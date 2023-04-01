// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in a definition graph for a property of an object.
/// </summary>
public class PropertyDefinitionNodeFlattened : DefinitionNodeFlattened
{
    /// <summary>
    /// Gets or sets the NodeKey for the <see cref="TypeDefinitionNodeFlattened"/> the type of the property this node represents.
    /// </summary>
    public string TypeNodeKey { get; set; } = string.Empty;
}
