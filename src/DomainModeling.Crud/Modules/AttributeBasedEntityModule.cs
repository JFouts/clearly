// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace DomainModeling.Crud;

public class AttributeBasedEntityModule : EntityModule
{
    public override void OnApplyingModule(EntityDefinition entity)
    {
        var definitionAttributes = entity.Entity.GetCustomAttributes<EntityDefinitionAttribute>();

        foreach (var definitionAttribute in definitionAttributes)
        {
            definitionAttribute.ApplyToEntityDefinition(entity);
        }
    }
}
