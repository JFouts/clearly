// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;

namespace Clearly.Crud;

/// <summary>
/// Configures the defintion for a field on an entity when it is built
/// </summary>
/// <typeparam name="TEntity">Entity Model that is being configured</typeparam>
public class EntityFieldDefinitionBuilder<TEntity>
    where TEntity : IEntity
{
    private readonly EntityDefinition entity;
    private readonly EntityFieldDefinition field;

    internal EntityFieldDefinitionBuilder(EntityDefinition entity, EntityFieldDefinition field)
    {
        this.entity = entity;
        this.field = field;
    }

    /// <summary>
    /// Applies an attribute based <see cref="EntityFieldDefinitionAttribute"> field defintion to this field
    /// </summary>
    /// <param name="attribute">An attribute that will be applied to modify the defintion of this field</param>
    /// <returns>The builder for fluent chaining.</returns>
    public EntityFieldDefinitionBuilder<TEntity> ApplyAttribute(EntityFieldDefinitionAttribute attribute)
    {
        attribute.ApplyToEntityFieldDefinition(entity, field);

        return this;
    }
}
