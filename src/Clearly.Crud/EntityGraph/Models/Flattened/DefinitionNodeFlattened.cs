// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;

namespace Clearly.Crud.EntityGraph;

/// <summary>
/// Represents a node in a type's definition graph.
/// </summary>
public abstract class DefinitionNodeFlattened
{
    /// <summary>
    /// Gets or sets a unique identifier representing this node.
    /// </summary>
    public string NodeKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display name of the field.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of features that are defined for this graph node.
    /// </summary>
    public Dictionary<string, JToken> Features { get; set; } = new Dictionary<string, JToken>();
}
