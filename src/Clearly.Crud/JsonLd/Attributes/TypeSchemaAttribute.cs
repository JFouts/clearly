// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Sets the IRI for the type and assumes all terms on the entity come from a shared
/// vocab as defined by that IRI. You can explicitly set the term vocab if the terms
/// are not based off the type's IRI.
/// </summary>
/// <remarks>
/// For example:
/// Iri of https://schema.org/Person
/// will by default have a TermsVocab of http://schema.org/Person#
/// 
/// However to conform with the conventions set by schema.org you should set the TermVocab
/// to http://schema.org/
/// 
/// This way for example term "name" will resolve to  http://schema.org/name rather than http://schema.org/Person#name
/// </remarks>
public class TypeSchemaAttribute : ObjectTypeDefinitionNodeAttribute
{
    /// <summary>
    /// The IRI (typically a URL) uniquely identifying this type.
    /// </summary>
    public string Iri { get; set; } = string.Empty;

    /// <summary>
    /// The base IRI applied to terms/types under this type. By default with will be based off the type IRI in the form of "{IRI}#{term}"
    /// </summary>
    public string DefaultVocab { get; set; } = string.Empty;

    /// <summary>
    /// Sets the IRI for the type and assumes all terms on the entity come from a shared
    /// vocab as defined by that IRI. You can explicitly set the term vocab if the terms
    /// are not based off the type's IRI.
    /// </summary>
    /// <remarks>
    /// For example:
    /// Iri of https://schema.org/Person
    /// will by default have a TermVocab of http://schema.org/Person#
    /// 
    /// However to conform with the conventions set by schema.org you should set the TermVocab
    /// to http://schema.org/
    /// 
    /// This way for example term "name" will resolve to  http://schema.org/name rather than http://schema.org/Person#name
    /// </remarks>
    /// <param name="iri">The IRI (typically a URL) uniquely identifying this type.</param>
    public TypeSchemaAttribute(string iri)
    {
        Iri = iri;
    }

    /// <inheritdoc />
    protected internal override void ApplyToDefinition(ObjectTypeDefinitionNode entity)
    {
        var jsonLdFeature = entity.Using<JsonLdTypeFeature>();
        jsonLdFeature.TypeIri = Iri;

        var vocab = string.IsNullOrEmpty(DefaultVocab) ? $"{Iri}#" : DefaultVocab;
        jsonLdFeature.TermsDefaultVocab = vocab;
    }
}
