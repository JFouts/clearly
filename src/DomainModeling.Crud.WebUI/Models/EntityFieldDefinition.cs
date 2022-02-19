using System.Reflection;
using DomainModeling.Crud.WebUi.Utilities;

namespace DomainModeling.Crud.WebUi;

public class EntityFieldDefinition : BaseDefinition
{
    public PropertyInfo Property { get; set; }
    public string DisplayName { get; set; } = string.Empty;

    public EntityFieldDefinition(PropertyInfo property)
    {
        Property = property;
        DisplayName = property.Name.SplitCamelCase();
    }
}
