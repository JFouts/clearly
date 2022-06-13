// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Sets the Blazor Component the should be used to render editor for this field.
/// </summary>
/// <remarks>
/// For the list of system editor components.
/// </remarks>
public class FieldEditorAttribute : FieldDefinitionAttribute
{
    /// <summary>
    /// Gets or sets the name of the Blazor Component that renders this feild in the admin.
    /// </summary>
    public string EditorComponentName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldEditorAttribute"/> class.
    /// </summary>
    /// <param name="viewComponentName">The name of the View Component that renders this feild in the admin.</param>
    public FieldEditorAttribute(string viewComponentName)
    {
        EditorComponentName = viewComponentName;
    }

    /// <inheritdoc/>
    protected override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        metadata.EditorComponentName = EditorComponentName;
    }
}
