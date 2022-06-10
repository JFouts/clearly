// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

public class CrudAdminEntityModule : EntityModule
{
    /// <inheritdoc/>
    public override void OnApplyingFallbackDefaults(EntityDefinition entity)
    {
        var metadata = entity.Using<CrudAdminEntityFeature>();

        if (string.IsNullOrWhiteSpace(metadata.DataSourceUrl))
        {
            metadata.DataSourceUrl = $"/api/{entity.NameKey.ToLower()}";
        }
    }
}
