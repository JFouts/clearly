// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace Clearly.Crud;

public class AttributeBasedEntityFieldModule : EntityFieldModule
{
    public override void OnApplyingModule(EntityDefinition entity, FieldDefinition field)
    {
        var definitionAttributes = field.Property.GetCustomAttributes<FieldDefinitionAttribute>();

        foreach (var definitionAttribute in definitionAttributes)
        {
            definitionAttribute.ApplyToFieldDefinition(entity, field);
        }
    }
}
