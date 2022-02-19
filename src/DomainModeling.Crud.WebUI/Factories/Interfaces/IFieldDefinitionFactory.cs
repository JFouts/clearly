using System.Reflection;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IFieldDefinitionFactory : IService
{
    EntityFieldDefinition CreateFieldDefinition(PropertyInfo property);
}
