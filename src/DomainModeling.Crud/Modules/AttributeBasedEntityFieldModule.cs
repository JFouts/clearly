using System.Reflection;

namespace DomainModeling.Crud;

public class AttributeBasedEntityFieldModule : EntityFieldModule
{   public override void OnApplyingModule(EntityDefinition entity, EntityFieldDefinition field)
    {
        var definitionAttributes = field.Property.GetCustomAttributes<EntityFieldDefinitionAttribute>();

        foreach (var DefinitionAttribute in definitionAttributes)
        {
            DefinitionAttribute.ApplyToEntityFieldDefinition(entity, field);
        }
    }
}
