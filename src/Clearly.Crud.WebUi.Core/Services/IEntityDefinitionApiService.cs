using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi.Core.Services;

public interface IEntityDefinitionApiService
{
    Task<TypeDefinitionNodeFlattened> GetById(string entityNameKey);
}
