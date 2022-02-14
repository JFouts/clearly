using DomainModeling.Core;
using System.Reflection;

namespace DomainModeling.Crud.Infrastructure;

/// <summary>
/// Locates IEntity types within the registered assemblies
/// </summary>
internal class EntitiesInAssemblyProvider : ITypeProvider
{
    private IEnumerable<Assembly> _assemblies;

    /// <summary>
    /// Locates IEntity types within the registered assemblies
    /// </summary>
    /// <param name="assemblies">The assemblies to search</param>
    public EntitiesInAssemblyProvider(IEnumerable<Assembly> assemblies)
    {
        _assemblies = assemblies;
    }

    /// <summary>
    /// Gets all IEntity types within registered assemblies
    /// </summary>
    /// <returns>List of types from the registered assemblies.</returns>
    public IEnumerable<Type> GetTypes()
    {
        return _assemblies
            .SelectMany(x => x.GetExportedTypes())
            .Where(x => x.IsAssignableTo(typeof(IEntity)));
    }
}