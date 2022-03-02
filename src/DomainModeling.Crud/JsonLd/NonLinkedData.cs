// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

/// <summary>
/// Removes the property from the linked data schema for JSON-LD
/// </summary>
public class NonLinkedData : EntityFieldDefinitionAttribute
{
    /// <inheritdoc />
    protected internal override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        field.UsingMetadata<JsonLdFieldMetadata>().ExcludeFromLinkedData = true;
    }
}
