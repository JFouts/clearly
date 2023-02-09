// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in an type's definition graph for a type.
/// </summary>
public class TypeDefinitionNodeFlattened : DefinitionNodeFlattened
{
    public TypeDefinitionNodeType NodeType { get; set; }
    public IEnumerable<PropertyDefinitionNodeFlattened> Properties { get; set; } = new List<PropertyDefinitionNodeFlattened>();
}
