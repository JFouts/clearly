// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

public class MultiSelectFieldEditor : FieldDefinitionAttribute
{
    private readonly string options;

    /// <summary>
    /// Sets the Editor Component for this Field in the Admin to use a Multi-Select Editor.
    /// </summary>
    /// <param name="options">A static set of options in the for of key value pairs. ex. "key1,value1;key2,value2"</param>
    public MultiSelectFieldEditor(string options)
    {
        this.options = options;
    }

    /// <inheritdoc/>
    protected override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        var adminFieldFeature = field.Using<CrudAdminFieldFeature>();

        adminFieldFeature.EditorComponentName = SystemViewComponents.MultiSelectList;

        var dropDownFeature = field.Using<CrudAdminDropDownFeature>();
        dropDownFeature.DataSource = options;
        dropDownFeature.DataSourceType = "StaticList"; // TODO: Enum or constants for this value
    }
}

public class DropDownFieldEditorAttribute : FieldDefinitionAttribute
{
    private readonly string options;

    /// <summary>
    /// Sets the Editor Component for this Field in the Admin to use a Drop Down Editor.
    /// </summary>
    /// <param name="options">A static set of options in the for of key value pairs. ex. "key1,value1;key2,value2"</param>
    public DropDownFieldEditorAttribute(string options)
    {
        this.options = options;
    }

    /// <inheritdoc/>
    protected override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        var adminFieldFeature = field.Using<CrudAdminFieldFeature>();

        adminFieldFeature.EditorComponentName = SystemViewComponents.DropDownList;

        var dropDownFeature = field.Using<CrudAdminDropDownFeature>();
        dropDownFeature.DataSource = options;
        dropDownFeature.DataSourceType = "StaticList"; // TODO: Enum or constants for this value
    }
}

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
