// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.JsonLd;

public class TermSchemaAttribute : EntityFieldDefinitionAttribute
{
    public string Iri { get; set; } = string.Empty;

    public TermSchemaAttribute(string iri)
    {
        Iri = iri;
    }

    protected internal override void ApplyToEntityFieldDefinition(EntityDefinition entity, EntityFieldDefinition field)
    {
        field.Using<JsonLdFieldFeature>().Iri = Iri;
    }
}
