// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Provides JSON-LD for a specific field on an entity
/// </summary>
public record JsonLdFieldFeature : IDefinitionFeature
{
    public string Iri { get; set; } = string.Empty;
    public bool ExcludeFromLinkedData { get; set; }
}
