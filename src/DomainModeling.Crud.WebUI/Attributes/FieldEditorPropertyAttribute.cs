// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi;

public class FieldEditorPropertyAttribute : EntityFieldDefinitionAttribute
{
    public string Name { get; set; } = string.Empty;
    public object Value { get; set; } = string.Empty;

    public FieldEditorPropertyAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }

    protected override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        var metadata = field.UsingMetadata<CrudAdminEntityFieldMetadata>();

        metadata.EditorProperties[Name] = Value;
    }
}
