using Clearly.Crud.EntityGraph;

namespace Clearly.Crud.WebUi.Core.Services;

public interface IEntityDefinitionApiService
{
    Task<Dictionary<string, TypeDefinitionNodeFlattened>> GetById(string entityNameKey);
}
