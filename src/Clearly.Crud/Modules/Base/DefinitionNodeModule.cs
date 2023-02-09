// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

public abstract class DefinitionNodeModule<TNode> : IDefinitionNodeModule
    where TNode : DefinitionNode
{
    public virtual void OnApplyingModule(TNode node) { }
    public virtual void OnApplyingFallbackDefaults(TNode node) { }

    public void OnApplyingModule(DefinitionNode node)
    {
        if (node is TNode typedNode)
        {
            OnApplyingModule(typedNode);
        }
    }

    public void OnApplyingFallbackDefaults(DefinitionNode node)
    {
        if (node is TNode typedNode)
        {
            OnApplyingFallbackDefaults(typedNode);
        }
    }
}
