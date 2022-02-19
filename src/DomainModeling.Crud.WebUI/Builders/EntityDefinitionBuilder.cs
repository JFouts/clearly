using System.Linq.Expressions;
using DomainModeling.Core;

namespace DomainModeling.Crud.WebUi;

public class EntityDefinitionBuilder<TEntity> where TEntity : IEntity
{
    private readonly EntityDefinition _definition;

    public EntityDefinitionBuilder(EntityDefinition definition)
    {
        _definition = definition;
    }

    public EntityFieldDefinitionBuilder<TEntity> Field<TProp>(Expression<Func<TEntity, TProp>> selector)
    {
        var selectedPropertyInfo = selector.GetPropertyInfo();
        var selectedField = _definition.Fields.FirstOrDefault(x => x.Property.Name == selectedPropertyInfo.Name);

        if (selectedField == null)
        {
            // TODO: Better exceptions
            throw new Exception();
        }

        return new EntityFieldDefinitionBuilder<TEntity>(_definition, selectedField);
    }

    public EntityDefinitionBuilder<TEntity> ApplyAttribute(EntityDefinitionAttribute attribute)
    {
        attribute.ApplyToEntityDefinition(_definition);

        return this;
    }
}
