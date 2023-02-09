// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

/// <summary>
/// Applies any changes to the Definition caused by this attribute
/// </summary>
[AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class PropertyDefinitionNodeAttribute : DefinitionNodeAttribute
{
    /// <summary>
    /// Applies any changes to the Definition caused by this attribute
    /// </summary>
    /// <param name="node">The definition node being built</param>
    protected internal abstract void ApplyToDefinition(PropertyDefinitionNode node);

    protected internal override void ApplyToDefinition(DefinitionNode node)
    {
        if (node is PropertyDefinitionNode typedNode)
        {
            ApplyToDefinition(typedNode);
        }
    }
}
