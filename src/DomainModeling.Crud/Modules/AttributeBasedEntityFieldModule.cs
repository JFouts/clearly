// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace DomainModeling.Crud;

public class AttributeBasedEntityFieldModule : EntityFieldModule
{
    public override void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field)
    {
        var definitionAttributes = field.Property.GetCustomAttributes<EntityFieldDefinitionAttribute>();

        foreach (var definitionAttribute in definitionAttributes)
        {
            definitionAttribute.ApplyToEntityFieldDefinition(entity, field);
        }
    }
}
