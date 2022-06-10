// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.WebUi;

/// <summary>
/// Applies a custom property for the <see cref="ViewComponent" /> that renders this feild in the admin.
/// </summary>
public class FieldEditorPropertyAttribute : FieldDefinitionAttribute
{
    /// <summary>
    /// Gets or sets the name of the property being set by this attribute.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the property being set by this attribute.
    /// </summary>
    public object Value { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldEditorPropertyAttribute"/> class.
    /// </summary>
    /// <param name="name">The name of the property being set by this attribute.</param>
    /// <param name="value">The value of the property being set by this attribute.</param>
    public FieldEditorPropertyAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }

    /// <inheritdoc />
    protected override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        metadata.EditorProperties[Name] = Value;
    }
}
