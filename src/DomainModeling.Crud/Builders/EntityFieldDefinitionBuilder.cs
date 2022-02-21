using DomainModeling.Core;

namespace DomainModeling.Crud;

/// <summary>
/// Configures the defintion for a field on an entity when it is built
/// </summary>
/// <typeparam name="TEntity">Entity Model that is being configured</typeparam>
public class EntityFieldDefinitionBuilder<TEntity> where TEntity : IEntity
{
    private readonly EntityDefinition _entity;
    private readonly EntityFieldDefinition _field;

    internal EntityFieldDefinitionBuilder(EntityDefinition entity, EntityFieldDefinition field)
    {
        _entity = entity;
        _field = field;
    }

    /// <summary>
    /// Applies an attribute based <see cref="EntityFieldDefinitionAttribute"> field defintion to this field
    /// </summary>
    /// <param name="attribute">An attribute that will be applied to modify the defintion of this field</param>
    /// <returns>The builder for fluent chaining.</returns>
    public EntityFieldDefinitionBuilder<TEntity> ApplyAttribute(EntityFieldDefinitionAttribute attribute)
    {
        attribute.ApplyToEntityFieldDefinition(_entity, _field);

        return this;
    }
}
