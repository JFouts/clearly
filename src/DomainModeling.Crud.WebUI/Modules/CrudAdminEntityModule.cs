// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi;

public class CrudAdminEntityModule : EntityModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
        var metadata = entity.UsingMetadata<CrudAdminEntityMetadata>();

        if (string.IsNullOrWhiteSpace(metadata.DataSourceUrl))
        {
            metadata.DataSourceUrl = $"/api/{entity.NameKey.ToLower()}";
        }
    }
}
