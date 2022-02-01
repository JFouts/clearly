using System.Reflection;
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
        var entities = _typeProvider.GetTypes();

        Console.WriteLine("Adding Dynamic Controllers");

        foreach (var applicationPart in parts.OfType<AssemblyPart>())
        {
            var assembly = applicationPart.Assembly;
            
            Console.WriteLine($"\tAssembly: {assembly.FullName}");

            var controllers = from type in assembly.GetTypes()
                where Attribute.IsDefined(type, typeof(GenericEntityRouteAttribute)) 
                    && type.IsGenericType
                select type;

            foreach (var controller in controllers)
            {
                Console.WriteLine($"\t\tController: {controller.FullName}");

                foreach (var type in entities)
                {
                    Console.WriteLine($"\t\t\tEntity: {type.FullName}");

                    var typedController = controller.MakeGenericType(type).GetTypeInfo();
                    feature.Controllers.Add(typedController);
                }
            }
        }
    }
}
