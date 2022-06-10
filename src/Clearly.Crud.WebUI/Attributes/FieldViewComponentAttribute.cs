// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Sets the <see cref="ViewComponent" /> the should be used to render editor for this field.
/// </summary>
/// <remarks>
/// For the list of system view components see TODO: URL.
/// </remarks>
public class FieldEditorAttribute : FieldDefinitionAttribute
{
    /// <summary>
    /// Gets or sets the name of the View Component that renders this feild in the admin.
    /// </summary>
    public string ViewComponentName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldEditorAttribute"/> class.
    /// </summary>
    /// <param name="viewComponentName">The name of the View Component that renders this feild in the admin.</param>
    public FieldEditorAttribute(string viewComponentName)
    {
        ViewComponentName = viewComponentName;
    }

    /// <inheritdoc/>
    protected override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        metadata.EditorViewComponentName = ViewComponentName;
    }
}
