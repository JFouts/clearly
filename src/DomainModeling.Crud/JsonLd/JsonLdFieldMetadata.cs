// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

public record JsonLdFieldMetadata : IMetadata
{
    public string Iri { get; set; } = string.Empty;
}
