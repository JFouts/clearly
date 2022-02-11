using System.Reflection;
using DomainModeling.Crud.WebUi.Models;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IFieldDefinitionFactory : IService
{
    FieldDefintion CreateFieldDefinition(PropertyInfo property);
}
