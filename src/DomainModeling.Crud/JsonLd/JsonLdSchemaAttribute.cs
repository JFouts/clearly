// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

public class JsonLdSchemaAttribute : EntityFieldDefinitionAttribute
{
    public string Iri { get; set; } = string.Empty;

    protected internal override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        field.UsingMetadata<JsonLdFieldMetadata>().Iri = Iri;
    }
}
