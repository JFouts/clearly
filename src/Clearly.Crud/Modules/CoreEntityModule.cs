// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

public class CoreEntityModule : DefinitionNodeModule<EntityTypeDefinitionNode>
{
    public override void OnApplyingFallbackDefaults(EntityTypeDefinitionNode entity)
    {
        if (string.IsNullOrWhiteSpace(entity.DisplayName))
        {
            entity.DisplayName = entity.Type.Name.FormatForDisplay();
        }

        if (string.IsNullOrWhiteSpace(entity.NodeKey))
        {
            entity.NodeKey = entity.Type.Name;
        }
    }
}
