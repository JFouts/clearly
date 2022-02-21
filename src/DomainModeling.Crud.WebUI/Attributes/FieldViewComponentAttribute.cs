// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi;

/// <summary>
/// Sets the <see cref="ViewComponent"> the should be used to render editor for this field.
/// </summary>
/// <remarks>
/// For the list of system view components see TODO: URL
/// </remarks>
public class FieldEditorAttribute : EntityFieldDefinitionAttribute
{
    public string ViewComponentName { get; set; }

    public FieldEditorAttribute(string viewComponentName)
    {
        ViewComponentName = viewComponentName;
    }

    protected override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        var metadata = field.UsingMetadata<CrudAdminEntityFieldMetadata>();

        metadata.EditorViewComponentName = ViewComponentName;
    }
}
