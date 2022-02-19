using DomainModeling.Core;

namespace DomainModeling.Crud.WebUi;

public class EntityFieldDefinitionBuilder<TEntity> where TEntity : IEntity
{
    private readonly EntityDefinition _entity;
    private readonly EntityFieldDefinition _field;

    public EntityFieldDefinitionBuilder(EntityDefinition entity, EntityFieldDefinition field)
    {
        _entity = entity;
        _field = field;
    }

    public EntityFieldDefinitionBuilder<TEntity> ApplyAttribute(EntityFieldDefinitionAttribute attribute)
    {
        attribute.ApplyToEntityFieldDefinition(_entity, _field);

        return this;
    }
}
