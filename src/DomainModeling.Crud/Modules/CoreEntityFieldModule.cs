// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public class CoreEntityFieldModule : EntityFieldModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field)
    {
        if (string.IsNullOrWhiteSpace(field.DisplayName))
        {
            field.DisplayName = field.Property.Name.FormatForDisplay();
        }
    }
}