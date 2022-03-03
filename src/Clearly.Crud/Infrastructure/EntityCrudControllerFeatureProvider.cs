// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Clearly.Crud.Infrastructure;

/// <summary>
/// Adds support for generic controllers to the AspNetCore Controller Feature.
/// </summary>
internal class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly ITypeProvider typeProvider;

    /// <summary>
    /// Adds support for generic controllers to the AspNetCore Controller Feature.
    /// </summary>
    /// <param name="typeProvider">The source for the Types to register generic controllers with.</param>
    public GenericControllerFeatureProvider(ITypeProvider typeProvider)
    {
        this.typeProvider = typeProvider;
    }

    /// <summary>
    /// Updates the feature instance.
    /// </summary>
    /// <param name="parts">The list of Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPart instances in the application.</param>
    /// <param name="feature">The feature instance to populate.</param>
    /// <remarks>
    /// Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPart instances in parts
    /// appear in the same ordered sequence they are stored in Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager.ApplicationParts.
    /// This ordering may be used by the feature provider to make precedence decisions.
    /// </remarks>
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var allowedTypes = typeProvider.GetTypes();

        foreach (var applicationPart in parts.OfType<AssemblyPart>())
        {
            RegisterAllowedTypesToGenericControllersInAssembly(allowedTypes, applicationPart.Assembly, feature);
        }
    }

    private static void RegisterAllowedTypesToGenericControllersInAssembly(IEnumerable<Type> allowedTypes, Assembly assembly, ControllerFeature feature)
    {
        var controllers = GetTypesFromAssembly(assembly);

        foreach (var controller in controllers)
        {
            RegisterAllowedTypesToGenericController(allowedTypes, controller, feature);
        }
    }

    private static void RegisterAllowedTypesToGenericController(IEnumerable<Type> allowedTypes, Type controller, ControllerFeature feature)
    {
        foreach (var type in allowedTypes)
        {
            RegisterAllowedTypeToController(type, controller, feature);
        }
    }

    private static void RegisterAllowedTypeToController(Type allowedType, Type controller, ControllerFeature feature)
    {
        var typedController = controller.MakeGenericType(allowedType).GetTypeInfo();
        feature.Controllers.Add(typedController);
    }

    // TODO: Can this be pulled out of this class? Probably rework type provider to give the generic and the type
    private static IEnumerable<Type> GetTypesFromAssembly(Assembly assembly)
    {
        return
            from type in assembly.GetTypes()
            where Attribute.IsDefined(type, typeof(GenericEntityControllerAttribute))
                && type.IsGenericType
            select type;
    }
}
