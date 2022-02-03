using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DomainModeling.Crud.Infrastructure;

internal class GenericControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.IsGenericType)
        {
            var genericType = controller.ControllerType.GenericTypeArguments[0];
 
            var typeName = genericType.Name.ToLower();
            var routeAttribute = (RouteAttribute?) Attribute.GetCustomAttribute(controller.ControllerType, typeof(RouteAttribute));

            if (routeAttribute == null)
            {
                routeAttribute = new RouteAttribute(typeName);
            }
            else
            {
                var old = routeAttribute;

                if (routeAttribute.Template.Contains("{type}"))
                {
                    routeAttribute = new RouteAttribute(old.Template.Replace("{type}", typeName));
                }
                else
                {
                    routeAttribute = new RouteAttribute(old.Template.TrimEnd('/') + "/" + typeName);
                }

                routeAttribute.Name = old.Name;
                routeAttribute.Order = old.Order;
            }

            controller.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel(routeAttribute),
            });
        }
    }
}
