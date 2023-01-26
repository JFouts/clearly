// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud;

public class AttributeBasedEntityModule : DefinitionNodeModule<EntityTypeDefinitionNode>
{
    public override void OnApplyingModule(EntityTypeDefinitionNode entity)
    {
        var definitionAttributes = entity.Type.GetCustomAttributes<ObjectTypeDefinitionNodeAttribute>();

        foreach (var definitionAttribute in definitionAttributes)
        {
            definitionAttribute.ApplyToDefinition(entity);
        }
    }
}
