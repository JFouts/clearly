// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.JsonLd;

public record JsonLdTypeFeature : IDefinitionFeature
{
    public string TypeIri { get; set; } = string.Empty;
    public string TermsDefaultVocab { get; set; } = string.Empty;
}
