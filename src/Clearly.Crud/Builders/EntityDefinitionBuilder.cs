// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq.Expressions;
using Clearly.Core;

namespace Clearly.Crud;

/// <summary>
/// Configures the defintion for an entity when it is built.
/// </summary>
/// <typeparam name="TEntity">Entity Model that is being configured</typeparam>
public class EntityDefinitionBuilder<TEntity>
    where TEntity : IEntity
{
    private readonly EntityDefinition definition;

    internal EntityDefinitionBuilder(EntityDefinition definition)
    {
        this.definition = definition;
    }

    /// <summary>
    /// Selects a property field on the entity to be configured further.
    /// </summary>
    /// <typeparam name="TProp">The type of the property selected</typeparam>
    /// <param name="selector">An expression selecting a property on the entity</param>
    /// <returns>A field builder for continued configuration</returns>
    /// <exception cref="ArgumentException">Thrown when the selector expression is invalid</exception>
    /// <remarks>This must be a simple expression selecting a field. The expression will not be evaluated.</remarks>
    public EntityFieldDefinitionBuilder<TEntity> Field<TProp>(Expression<Func<TEntity, TProp>> selector)
    {
        var selectedPropertyInfo = selector.GetPropertyInfo();
        var selectedField = definition.Fields.First(x => x.Property.Name == selectedPropertyInfo.Name);

        return new EntityFieldDefinitionBuilder<TEntity>(definition, selectedField);
    }

    /// <summary>
    /// Applies an attribute based EntityDefinitionAttribute entity defintion to this entity
    /// </summary>
    /// <param name="attribute">An attribute that will be applied to modify the defintion of this entity</param>
    /// <returns>The builder for fluent chaining.</returns>
    public EntityDefinitionBuilder<TEntity> ApplyAttribute(EntityDefinitionAttribute attribute)
    {
        attribute.ApplyToEntityDefinition(definition);

        return this;
    }
}
