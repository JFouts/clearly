// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.JsonLd;

/// <summary>
/// Removes the property from the linked data schema for JSON-LD.
/// </summary>
public class NonLinkedDataAttribute : PropertyDefinitionNodeAttribute
{
    /// <inheritdoc />
    protected internal override void ApplyToDefinition(PropertyDefinitionNode property)
    {
        property.Using<JsonLdFieldFeature>().ExcludeFromLinkedData = true;
    }
}
