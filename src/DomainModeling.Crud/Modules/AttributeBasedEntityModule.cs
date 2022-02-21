using System.Reflection;

namespace DomainModeling.Crud;

public class AttributeBasedEntityModule : EntityModule
{
    public override void OnApplyingModule(EntityDefinition entity)
    {
        var definitionAttributes = entity.Entity.GetCustomAttributes<EntityDefinitionAttribute>();

        foreach (var DefinitionAttribute in definitionAttributes)
        {
            DefinitionAttribute.ApplyToEntityDefinition(entity);
        }
    }
}
