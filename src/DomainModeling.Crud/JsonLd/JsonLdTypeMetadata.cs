// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

public record JsonLdTypeMetadata : IMetadata
{
    public string TypeIri { get; set; } = string.Empty;
    public string TermsDefaultVocab { get; set; } = string.Empty;
}