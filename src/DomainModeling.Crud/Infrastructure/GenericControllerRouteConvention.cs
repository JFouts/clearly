using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DomainModeling.Crud.Infrastructure;

internal class GenericActionRouteConvention : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        if (action.Controller.ControllerType.IsGenericType)
        {
            var genericType = action.Controller.ControllerType.GenericTypeArguments[0];

            foreach (var selector in action.Selectors)
            {
                if (selector?.AttributeRouteModel != null 
                    && string.IsNullOrWhiteSpace(selector.AttributeRouteModel.Name)) 
                {
                    selector.AttributeRouteModel.Name = $"{action.Controller.ControllerType.Name}<{genericType.Name}>{action.ActionMethod.Name}";

                    Console.WriteLine($"Route Named [{selector.AttributeRouteModel.Name}]");
                }
            }
        }
    }
}

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

            if (string.IsNullOrWhiteSpace(routeAttribute.Name))
            {
                routeAttribute.Name = $"{controller.ControllerType.Name}<{genericType.Name}>";
            }

            Console.WriteLine($"Route Created [{routeAttribute.Name}:{routeAttribute.Order}][{controller.Selectors.Count}] - {routeAttribute.Template}");

            controller.Selectors.Clear();
            controller.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel(routeAttribute),
            });
        }
    }
}
