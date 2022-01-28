using DomainModeling.Core;
using System.Reflection;

namespace DomainModeling.Crud.Infrastructure;

internal class EntitiesInAssemblyProvider : ITypeProvider
{
    private IEnumerable<Assembly> _assemblies;

    public EntitiesInAssemblyProvider(IEnumerable<Assembly> assemblies)
    {
        _assemblies = assemblies;
    }

    public IEnumerable<Type> GetTypes()
    {
        return _assemblies
            .SelectMany(x => x.GetExportedTypes())
            .Where(x => x.IsAssignableTo(typeof(IEntity)));
    }
}