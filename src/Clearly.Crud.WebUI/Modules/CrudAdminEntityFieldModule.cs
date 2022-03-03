// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.WebUi.ViewComponents.FieldEditors;

namespace Clearly.Crud.WebUi;

public class CrudAdminEntityFieldModule : EntityFieldModule
{
    public override void OnApplyingFallbackDefaults(EntityDefinition entity, EntityFieldDefinition field)
    {
        var metadata = field.Using<CrudAdminEntityFieldFeature>();

        if (string.IsNullOrWhiteSpace(metadata.EditorViewComponentName))
        {
            metadata.EditorViewComponentName = nameof(InputFieldEditor);
        }

        if (string.IsNullOrWhiteSpace(metadata.DisplayTemplate))
        {
            metadata.DisplayTemplate = field.Property.PropertyType!.Name;
        }
    }
}
