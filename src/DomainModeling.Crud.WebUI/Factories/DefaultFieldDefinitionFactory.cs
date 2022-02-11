using System.Reflection;
using DomainModeling.Crud.WebUi.Models;

namespace DomainModeling.Crud.WebUi.Factories;

public class DefaultFieldDefinitionFactory : IFieldDefinitionFactory
{
    public FieldDefintion CreateFieldDefinition(PropertyInfo property)
    {
        return new FieldDefintion
        {
            FieldDisplayType = GetDefaultDisplayTemplateForType(property.PropertyType),
            FieldEditorType = property.PropertyType.Name,
            FieldName = property.Name
        };
    }

    private string GetDefaultDisplayTemplateForType(Type type)
    {
        if (type.IsAssignableTo(typeof(IEnumerable<string>)))
        {
            return "StringList";    
        }

        return type.Name;
    }
}
