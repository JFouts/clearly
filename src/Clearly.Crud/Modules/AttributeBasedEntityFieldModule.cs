// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

public class AttributeBasedEntityFieldModule : DefinitionNodeModule<PropertyDefinitionNode>
{
    public override void OnApplyingModule(PropertyDefinitionNode property)
    {
        var definitionAttributes = property.Property.GetCustomAttributes<PropertyDefinitionNodeAttribute>();

        foreach (var definitionAttribute in definitionAttributes)
        {
            definitionAttribute.ApplyToDefinition(property);
        }
    }
}
