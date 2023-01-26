// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Models.EntityGraph;

namespace Clearly.Crud;

/// <summary>
/// Configures the definition for a property on an entity when it is built
/// </summary>
/// <typeparam name="TEntity">Entity Model that is being configured</typeparam>
public class PropertyDefinitionBuilder<TEntity>
    where TEntity : IEntity
{
    private readonly PropertyDefinitionNode property;

    internal PropertyDefinitionBuilder(PropertyDefinitionNode property)
    {
        this.property = property;
    }

    /// <summary>
    /// Applies an attribute based <see cref="PropertyDefinitionAttribute"> property definition to this property
    /// </summary>
    /// <param name="attribute">An attribute that will be applied to modify the definition of this property</param>
    /// <returns>The builder for fluent chaining.</returns>
    public PropertyDefinitionBuilder<TEntity> ApplyAttribute(PropertyDefinitionNodeAttribute attribute)
    {
        attribute.ApplyToDefinition(property);

        return this;
    }
}
