// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Applies a custom Schema IRI for the JSON-LD term used for this property.
/// </summary>
public class TermSchemaAttribute : PropertyDefinitionNodeAttribute
{
    /// <summary>
    /// Gets or sets the IRI for the schema that defines terms for this property.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="TermSchemaAttribute"/> class.
    /// </summary>
    /// <param name="iri">The IRI for the schema that defines terms for this property.</param>
    public TermSchemaAttribute(string iri)
    {
        Iri = iri;
    }

    /// <inheritdoc/>
    protected internal override void ApplyToDefinition(PropertyDefinitionNode property)
    {
        property.Using<JsonLdFieldFeature>().Iri = Iri;
    }
}
