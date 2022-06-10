// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud;

/// <summary>
/// Factory to create fully initialized instances of <see cref="EntityDefinition"/>.
/// </summary>
public interface IEntityDefinitionFactory
{
    /// <summary>
    /// Creates a fully initialized instances of <see cref="EntityDefinition"/>.
    /// </summary>
    /// <param name="entity">The object type to create a definition for.</param>
    /// <returns>A fully initialized instances of <see cref="EntityDefinition"/>.</returns>
    EntityDefinition CreateForType(Type entity);
}
