// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Clearly.Crud.RestApi.Infrastructure;

/// <summary>
/// Adds token replacement support for the [type] token and updates the default route for
/// generic controllers to include the type in the route.
/// </summary>
internal class GenericControllerRouteConvention : IControllerModelConvention
{
    private const string TYPE_TOKEN = "type";
    private const string CONTROLLER_TOKEN = "generic";
    private const string DEFAULT_ROUTE_TEMPLATE = $"[{CONTROLLER_TOKEN}]/[{TYPE_TOKEN}]";

    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerType.IsGenericType)
        {
            return;
        }

        AddTypeReplacementToken(controller);
        ApplyDefaultRoute(controller);
    }

    private void AddTypeReplacementToken(ControllerModel controller)
    {
        var genericType = controller.ControllerType.GenericTypeArguments[0];
        var typeName = genericType.Name.ToLower();

        controller.RouteValues[TYPE_TOKEN] = typeName;
        controller.RouteValues[CONTROLLER_TOKEN] = GetGenericControllerName(controller.ControllerType.Name);
    }

    private void ApplyDefaultRoute(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel == null)
            {
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(DEFAULT_ROUTE_TEMPLATE));
            }
        }
    }

    private string GetGenericControllerName(string systemName)
    {
        var temp = Regex.Replace(systemName, @"(.+)`\d+$", @"$1");

        return Regex.Replace(temp, @"(.+)Controller$", @"$1")
            .ToLowerInvariant();
    }
}
