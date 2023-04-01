// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Core;

namespace Clearly.Crud.Infrastructure;

/// <summary>
/// Locates IEntity types within the registered assemblies
/// </summary>
public class EntitiesInAssemblyProvider : ITypeProvider
{
    private IEnumerable<Assembly> assemblies;
    private IEnumerable<Type> types;

    /// <summary>
    /// Locates IEntity types within the registered assemblies
    /// </summary>
    /// <param name="assemblies">The assemblies to search</param>
    public EntitiesInAssemblyProvider(IEnumerable<Assembly> assemblies)
    {
        this.assemblies = assemblies;

        this.types = assemblies
            .SelectMany(x => x.GetExportedTypes())
            .Where(x => x.IsAssignableTo(typeof(IEntity)))
            .ToList();
    }

    /// <summary>
    /// Gets all IEntity types within registered assemblies
    /// </summary>
    /// <returns>List of types from the registered assemblies.</returns>
    public IEnumerable<Type> GetTypes()
    {
        return types;
    }
}
