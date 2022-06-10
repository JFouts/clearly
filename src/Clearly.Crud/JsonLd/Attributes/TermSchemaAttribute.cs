// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Applies a custom Schema IRI for the JSON-LD term used for this property.
/// </summary>
public class TermSchemaAttribute : FieldDefinitionAttribute
{
    /// <summary>
    /// Gets or sets the IRI for the shema that defines terms for this feild.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="TermSchemaAttribute"/> class.
    /// </summary>
    /// <param name="iri">The IRI for the shema that defines terms for this feild.</param>
    public TermSchemaAttribute(string iri)
    {
        Iri = iri;
    }

    /// <inheritdoc/>
    protected internal override void ApplyToFieldDefinition(ObjectTypeDefinition entity, FieldDefinition field)
    {
        field.Using<JsonLdFieldFeature>().Iri = Iri;
    }
}
