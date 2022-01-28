using System.Reflection;
using DomainModeling.Crud.AddControllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DomainModeling.Crud.Infrastructure;

internal class EntityCrudControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly ITypeProvider _typeProvider;

    public EntityCrudControllerFeatureProvider(ITypeProvider typeProvider)
    {
        _typeProvider = typeProvider;
    }

    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {       
        foreach (var type in _typeProvider.GetTypes())
        {
            var typedController = typeof(EntityCrudController<>).MakeGenericType(type).GetTypeInfo();
            feature.Controllers.Add(typedController);
        }
    }
}
