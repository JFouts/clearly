// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

// TODO: In C# 11
// internal abstract class TypedDefinitionNodeAttribute<TNode> : DefinitionNodeAttribute
//     where TNode : DefinitionNode
// {   
//     /// <summary>
//     /// Applies any changes to the Definition caused by this attribute
//     /// </summary>
//     /// <param name="node">The definition node being built</param>
//     protected internal abstract void ApplyToDefinition(ObjectTypeDefinitionNode node);

//     protected internal override void ApplyToDefinition(DefinitionNode node)
//     {
//         if (node is ObjectTypeDefinitionNode typedNode)
//         {
//             ApplyToDefinition(typedNode);
//         }
//     }
// }

/// <summary>
/// Applies any changes to the Definition caused by this attribute
/// </summary>
[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public abstract class ObjectTypeDefinitionNodeAttribute : DefinitionNodeAttribute
{
    /// <summary>
    /// Applies any changes to the Definition caused by this attribute
    /// </summary>
    /// <param name="node">The definition node being built</param>
    protected internal abstract void ApplyToDefinition(ObjectTypeDefinitionNode node);

    protected internal override void ApplyToDefinition(DefinitionNode node)
    {
        if (node is ObjectTypeDefinitionNode typedNode)
        {
            ApplyToDefinition(typedNode);
        }
    }
}
