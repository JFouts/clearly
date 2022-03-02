// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public class CoreEntityModule : EntityModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
        if (string.IsNullOrWhiteSpace(entity.DisplayName))
        {
            entity.DisplayName = entity.Entity.Name.FormatForDisplay();
        }

        if (string.IsNullOrWhiteSpace(entity.NameKey))
        {
            entity.NameKey = entity.Entity.Name;
        }
    }
}
