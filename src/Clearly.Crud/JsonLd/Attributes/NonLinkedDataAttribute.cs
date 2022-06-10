// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Removes the property from the linked data schema for JSON-LD.
/// </summary>
public class NonLinkedDataAttribute : FieldDefinitionAttribute
{
    /// <inheritdoc />
    protected internal override void ApplyToFieldDefinition(ObjectTypeDefinition objectType, FieldDefinition field)
    {
        field.Using<JsonLdFieldFeature>().ExcludeFromLinkedData = true;
    }
}
