// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;

namespace Clearly.Crud.WebUi;

/// <summary>
/// Collection of extension methods for <see cref="EntityDefinition"/>
/// </summary>
public static class EntityDefinitionExtensions
{
    /// <summary>
    /// Creates a fully initialized instances of <see cref="EntityDefinition"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity to create a definition for.</typeparam>
    /// <param name="factory">The factory used for initialization of the <see cref="EntityDefinition"/>.</param>
    /// <returns>A fully initialized instances of <see cref="EntityDefinition"/>.</returns>
    public static EntityDefinition CreateForEntity<TEntity>(this IEntityDefinitionFactory factory)
        where TEntity : IEntity
    {
        return factory.CreateForType(typeof(TEntity));
    }

    /// <summary>
    /// Creates a fully initialized instances of <see cref="ObjectTypeDefinition"/>.
    /// </summary>
    /// <typeparam name="TObjectType">The object type to create a definition for.</typeparam>
    /// <param name="factory">The factory used for initialization of the <see cref="ObjectTypeDefinition"/>.</param>
    /// <returns>A fully initialized instances of <see cref="ObjectTypeDefinition"/>.</returns>
    public static ObjectTypeDefinition CreateForObjectType<TObjectType>(this IEntityDefinitionFactory factory)
        where TObjectType : class, new()
    {
        return factory.CreateForType(typeof(TObjectType));
    }
}
