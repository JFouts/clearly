// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq.Expressions;
using Clearly.Core;
using Clearly.Crud.EntityGraph;

namespace Clearly.Crud;

/// <summary>
/// Configures the definition for an entity when it is built.
/// </summary>
/// <typeparam name="TEntity">Entity Model that is being configured</typeparam>
public class EntityDefinitionBuilder<TEntity>
    where TEntity : IEntity
{
    private readonly EntityTypeDefinitionNode definition;

    internal EntityDefinitionBuilder(EntityTypeDefinitionNode definition)
    {
        this.definition = definition;
    }

    /// <summary>
    /// Selects a property on the entity to be configured further.
    /// </summary>
    /// <typeparam name="TProp">The type of the property selected</typeparam>
    /// <param name="selector">An expression selecting a property on the entity</param>
    /// <returns>A property builder for continued configuration</returns>
    /// <exception cref="ArgumentException">Thrown when the selector expression is invalid</exception>
    /// <remarks>This must be a simple expression selecting a property. The expression will not be evaluated.</remarks>
    public PropertyDefinitionBuilder<TEntity> Field<TProp>(Expression<Func<TEntity, TProp>> selector)
    {
        var selectedPropertyInfo = selector.GetPropertyInfo();
        var selectedProperty = definition.Properties.First(x => x.Property.Name == selectedPropertyInfo.Name);

        return new PropertyDefinitionBuilder<TEntity>(selectedProperty);
    }

    /// <summary>
    /// Applies an attribute based <see cref="EntityDefinitionAttribute" /> entity definition to this entity
    /// </summary>
    /// <param name="attribute">An attribute that will be applied to modify the definition of this entity</param>
    /// <returns>The builder for fluent chaining.</returns>
    public EntityDefinitionBuilder<TEntity> ApplyAttribute(ObjectTypeDefinitionNodeAttribute attribute)
    {
        attribute.ApplyToDefinition(definition);

        return this;
    }
}
