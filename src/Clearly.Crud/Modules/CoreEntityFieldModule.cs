// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

public class CoreEntityFieldModule : DefinitionNodeModule<PropertyDefinitionNode>
{
    public override void OnApplyingFallbackDefaults(PropertyDefinitionNode property)
    {
        if (string.IsNullOrWhiteSpace(property.DisplayName))
        {
            property.DisplayName = property.Property.Name.FormatForDisplay();
        }
    }
}
