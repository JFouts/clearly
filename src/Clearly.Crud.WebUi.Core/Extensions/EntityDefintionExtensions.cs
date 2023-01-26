// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud.WebUi;

/// <summary>
/// Collection of extension methods for <see cref="EntityTypeDefinitionNode"/>
/// </summary>
public static class EntityDefinitionExtensions
{
    /// <summary>
    /// Creates a fully initialized instances of <see cref="EntityTypeDefinitionNode"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity to create a definition for.</typeparam>
    /// <param name="factory">The factory used for initialization of the <see cref="EntityTypeDefinitionNode"/>.</param>
    /// <returns>A fully initialized instances of <see cref="EntityTypeDefinitionNode"/>.</returns>
    public static EntityTypeDefinitionNode CreateForEntity<TEntity>(this IEntityDefinitionGraphFactory factory)
        where TEntity : IEntity
    {
        if (factory.CreateForType(typeof(TEntity)) is EntityTypeDefinitionNode node)
        {
            return node;
        }

        // TODO: Better exceptions
        throw new Exception($"Could not generate a type definition for Type {typeof(TEntity).Name}");
    }

    /// <summary>
    /// Creates a fully initialized instances of <see cref="ObjectTypeDefinition"/>.
    /// </summary>
    /// <typeparam name="TObjectType">The object type to create a definition for.</typeparam>
    /// <param name="factory">The factory used for initialization of the <see cref="ObjectTypeDefinition"/>.</param>
    /// <returns>A fully initialized instances of <see cref="ObjectTypeDefinition"/>.</returns>
    public static ObjectTypeDefinitionNode CreateForObjectType<TObjectType>(this IEntityDefinitionGraphFactory factory)
        where TObjectType : class, new()
    {
        return factory.CreateForType(typeof(TObjectType));
    }
}
